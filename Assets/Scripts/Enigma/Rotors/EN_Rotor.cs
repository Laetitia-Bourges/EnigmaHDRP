using System.Collections.Generic;
using System;
using UnityEngine;

public class EN_Rotor : EN_Encodeur
{
    #region event
    public event Action<float> OnRotateRotor = null;
    #endregion

    #region F/P
    [SerializeField] char notche = ' ';
    [SerializeField] char startConfig = 'A';
    int currentRotation = 0;
    bool isReverse = false;
    float currentAngle = 0;

    public bool IsValid => !char.IsWhiteSpace(notche) && !char.IsWhiteSpace(startConfig);
    public char StartConfig => startConfig;
    #endregion

    #region Unity Methods
    private void Start()
    {
        if (!IsValid) return;
        OnRotateRotor += UpdateRotorRotation;
        notche = notche.ToString().ToUpper()[0];
        startConfig = startConfig.ToString().ToUpper()[0];
        ResetRotor();
    }
    private void Update() => OnRotateRotor?.Invoke(currentAngle);
    private void OnDestroy() => OnRotateRotor = null;
    #endregion

    #region Custom Methods
    /// <summary>
    /// Set the rotor's start configuration
    /// </summary>
    /// <param name="_newConfig"></param>
    public void SetConfig(char _newConfig) => startConfig = _newConfig;
    /// <summary>
    /// Reset the rotor
    /// </summary>
    public void ResetRotor()
    {
        if (!IsValid) return;
        currentRotation = EN_DataManager.Instance.Data.Alphabet.IndexOf(startConfig);
        currentAngle = 360 * currentRotation / 26f;
    }
    /// <summary>
    /// Generates the correspondance dictionary
    /// </summary>
    /// <param name="_data">Data to create the dictionary</param>
    public void GenerateCorrespondance(string _data)
    {
        if (!IsValid) return;
        for (int i = 0; i < _data.Length; i++)
            correspondance.Add(EN_DataManager.Instance.Data.Alphabet[i], _data[i]);
    }
    #region Rotor Rotation
    /// <summary>
    /// Update the number of rotation of te rotor and his angle of rotation
    /// </summary>
    /// <returns>true if the rotor is at his notche, false else</returns>
    public bool Rotation()
    {
        if (!IsValid) return false;
        currentRotation += 1;
        currentRotation %= 26;
        currentAngle = 360 * currentRotation / 26f;
        int _notcheCorrespondance = currentRotation == 0 ? 25 : currentRotation - 1;
        return EN_DataManager.Instance.Data.Alphabet[_notcheCorrespondance] == notche;
    }
    /// <summary>
    /// Rotate the rotor
    /// </summary>
    /// <param name="_angle">angle maximal for the rotor</param>
    public void UpdateRotorRotation(float _angle)
    {
        Quaternion _direction = Quaternion.Euler(0, 0, 360 - _angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, _direction, Time.deltaTime * 20);
    }
    #endregion
    #region Encode Letter
    /// <summary>
    /// Get the char correspond at one index
    /// </summary>
    /// <param name="_index">index of the character</param>
    /// <returns>the caracter actually positionned at the index</returns>
    public char GetCharForIndex (int _index)
    {
        int _indexAlphabet = _index + currentRotation;
        _indexAlphabet = _indexAlphabet > 25 ? _indexAlphabet - 26 : _indexAlphabet;
        return EN_DataManager.Instance.Data.Alphabet[_indexAlphabet];
    }
    /// <summary>
    /// Get the rotor index of a character
    /// </summary>
    /// <param name="_char">char for which we need the index</param>
    /// <returns>the index of the rotor where the char is actually positionned</returns>
    public int GetIndexForChar(char _char)
    {
        int _index = EN_DataManager.Instance.Data.Alphabet.IndexOf(_char) - currentRotation;
        return _index < 0 ? _index + 26 : _index;
    }
    /// <summary>
    /// Encode a char with his correspondant char
    /// </summary>
    /// <param name="_index">index of the char to encode</param>
    /// <returns>the encoded char</returns>
    public override int Encode(int _index)
    {
        if (!IsValid) return _index;
        char _toDecode = GetCharForIndex(_index);
        char _codedChar = isReverse ? CodedLetterInReverse(_toDecode) : correspondance[_toDecode];
        isReverse = !isReverse;
        return GetIndexForChar(_codedChar);
    }
    /// <summary>
    /// Get the correspondant char in the reverse way
    /// </summary>
    /// <param name="_toDecode">char to decode</param>
    /// <returns>the correspondant char</returns>
    char CodedLetterInReverse(char _toDecode)
    {
        foreach (KeyValuePair<char, char> _charAssociation in correspondance)
            if (_charAssociation.Value == _toDecode)
                return _charAssociation.Key;
        return ' ';
    }
    #endregion
    #endregion
}
