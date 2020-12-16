using System.Collections.Generic;
using System;
using UnityEngine;

public class EN_Rotor : EN_Encodeur
{
    public event Action<float> OnRotateRotor = null;

    #region F/P
    [SerializeField] char notche = ' ';
    [SerializeField] char startConfig = 'A';
    int currentRotation = 0;
    bool isReverse = false;
    float currentAngle = 0;

    public bool IsValid => !char.IsWhiteSpace(notche) && !char.IsWhiteSpace(startConfig);
    #endregion

    #region Unity Methods
    private void Start()
    {
        if (!IsValid) return;
        OnRotateRotor += UpdateRotorRotation;
        notche = notche.ToString().ToUpper()[0];
        startConfig = startConfig.ToString().ToUpper()[0];
        InitRotor();
    }
    private void Update()
    {
        OnRotateRotor?.Invoke(currentAngle);
    }
    private void OnDestroy()
    {
        OnRotateRotor = null;
    }
    #endregion

    #region Custom Methods
    void InitRotor()
    {
        if (!IsValid) return;
        currentRotation = EN_DataManager.Instance.Data.Alphabet.IndexOf(startConfig);
        currentAngle = 360 * currentRotation / 26f;
    }
    public void GenerateCorrespondance(string _data)
    {
        if (!IsValid) return;
        for (int i = 0; i < _data.Length; i++)
            correspondance.Add(EN_DataManager.Instance.Data.Alphabet[i], _data[i]);
    }
    public bool Rotation()
    {
        if (!IsValid) return false;
        currentRotation += 1;
        currentRotation %= 26;
        currentAngle = 360 * currentRotation / 26f;
        //Debug.Log($"{name} : nb rot = {currentRotation} / lettre = {EN_DataManager.Instance.Data.Alphabet[currentRotation]} / notche = {notche}");
        int _notcheCorrespondance = currentRotation == 0 ? 25 : currentRotation - 1;
        return EN_DataManager.Instance.Data.Alphabet[_notcheCorrespondance] == notche;
    }
    public void UpdateRotorAngle()
    {
        float angle = 360 / 26f * currentRotation;
        transform.eulerAngles = new Vector3(0, 0, -angle);
    }
    public void UpdateRotorRotation(float _angle)
    {
        Quaternion _direction = Quaternion.Euler(0, 0, 360 - _angle);
        transform.rotation = Quaternion.Lerp(transform.rotation, _direction, Time.deltaTime * 20);
    }
    public char GetCharForIndex (int _index)
    {
        int _indexAlphabet = _index + currentRotation;
        _indexAlphabet = _indexAlphabet > 25 ? _indexAlphabet - 26 : _indexAlphabet;
        return EN_DataManager.Instance.Data.Alphabet[_indexAlphabet];
    }
    public int GetIndexForChar(char _char)
    {
        int _index = EN_DataManager.Instance.Data.Alphabet.IndexOf(_char) - currentRotation;
        return _index < 0 ? _index + 26 : _index;
    }

    public override int Encode(int _index)
    {
        if (!IsValid) return _index;
        char _toDecode = GetCharForIndex(_index);
        char _codedChar = isReverse ? CodedLetterInReverse(_toDecode) : correspondance[_toDecode];
        isReverse = !isReverse;
        //Debug.Log($"{name} encodage : index {_index} => {_toDecode} devient {_codedChar} => {GetIndexForChar(_codedChar)}");
        return GetIndexForChar(_codedChar);
    }

    char CodedLetterInReverse(char _toDecode)
    {
        foreach (KeyValuePair<char, char> _charAssociation in correspondance)
            if (_charAssociation.Value == _toDecode)
                return _charAssociation.Key;
        return ' ';
    }

    public void Reset()
    {
        InitRotor();
    }
    #endregion
}
