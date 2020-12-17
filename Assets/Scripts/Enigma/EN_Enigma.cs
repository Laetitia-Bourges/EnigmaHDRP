using System;
using System.Collections.Generic;
using UnityEngine;

public class EN_Enigma : EN_Singleton<EN_Enigma>, IEncode<char>
{
    #region Event
    public event Action<char> OnIsALetterEnter = null;
    public event Action<char> OnLetterIsEncoded = null;
    #endregion

    #region F/P
    [SerializeField] EN_Reflecteur reflecteur = null;
    [SerializeField] List<EN_Rotor> rotors = new List<EN_Rotor>();
    [SerializeField] AudioClip rotorRotationFeedbackSound = null;

    public bool IsValid => reflecteur && rotors.Count > 0 && rotorRotationFeedbackSound;
    public List<EN_Rotor> Rotors => rotors;
    #endregion

    #region Unity Methods
    protected override void Awake()
    {
        base.Awake();
        if (!IsValid) return;
        OnIsALetterEnter += (c) =>
        {
            RotateRotors();
            EN_SoundsManager.Instance?.PlaySound(rotorRotationFeedbackSound);
            EN_KeyboardAction.Instance?.UpdateKeyBoard(c);
        };
        OnLetterIsEncoded += (c) => EN_LightsManager.Instance?.SetLightPosition(c);
    }
    void Start()
    {
        if (!IsValid) return;
        List<string> _data = EN_DataManager.Instance.Data.RotorsData;
        for (int i = 0; i < rotors.Count; i++)
            rotors[i].GenerateCorrespondance(_data[i]);
    }
    void OnDestroy()
    {
        OnLetterIsEncoded = null;
        OnIsALetterEnter = null;
    }
    #endregion

    #region Custom Methods
    /// <summary>
    /// Encode a letter and return her correspondance
    /// </summary>
    /// <param name="_letter">char to encode</param>
    /// <returns>the encoded letter</returns>
    public char Encode(char _letter)
    {
        if (!IsValid) return ' ';
        _letter = _letter.ToString().ToUpper()[0];
        OnIsALetterEnter?.Invoke(_letter);
        char _codedLetter = EncodeLetter(_letter);
        OnLetterIsEncoded?.Invoke(_codedLetter);
        return _codedLetter;
    }
    /// <summary>
    /// Encode a letter and return her correspondance
    /// </summary>
    /// <param name="_letter">char to encode</param>
    /// <returns>the encoded letter</returns>
    char EncodeLetter(char _letter)
    {
        int _codeIndex = EN_DataManager.Instance.Data.Alphabet.IndexOf(_letter);
        for (int i = 0; i < rotors.Count; i++)
        {
            _codeIndex = rotors[i].Encode(_codeIndex);
        }
        _codeIndex = reflecteur.Encode(_codeIndex);
        for (int i = rotors.Count - 1; i >= 0; i--)
            _codeIndex = rotors[i].Encode(_codeIndex);

        return EN_DataManager.Instance.Data.Alphabet[_codeIndex];
    }
    /// <summary>
    /// Rotate all the rotors of the machine
    /// </summary>
    public void RotateRotors()
    {
        for (int i = 0; i < rotors.Count; i++)
        {
            bool _rotateNext = rotors[i].Rotation();
            if (!_rotateNext) return;
        }
    }
    /// <summary>
    /// Reset the rotors of Enigma
    /// </summary>
    public void ResetEnigma()
    {
        for (int i = 0; i < rotors.Count; i++)
            rotors[i].ResetRotor();
        EN_SoundsManager.Instance?.PlaySound(rotorRotationFeedbackSound);
    }
    /// <summary>
    /// Set the start configuration of the rotors
    /// </summary>
    /// <param name="_rotor1Config">new configuration for the rotor 1</param>
    /// <param name="_rotor2Config">new configuration for the rotor 2</param>
    /// <param name="_rotor3Config">new configuration for the rotor 3</param>
    public void ChangeRotorConfiguration(List<char> _newConfig)
    {
        for (int i = 0; i < _newConfig.Count; i++)
            rotors[i].SetConfig(_newConfig[i]);
    }
    #endregion
}
