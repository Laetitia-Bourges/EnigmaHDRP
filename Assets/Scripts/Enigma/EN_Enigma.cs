using System;
using System.Collections.Generic;
using UnityEngine;

public class EN_Enigma : EN_Singleton<EN_Enigma>, IEncode<char>
{
    #region Event
    public event Action OnRotateRotors = null;
    public event Action<char> OnLetterEncoded = null;
    #endregion

    #region F/P
    [SerializeField] EN_Reflecteur reflecteur = null;
    [SerializeField] List<EN_Rotor> rotors = new List<EN_Rotor>();
    [SerializeField] AudioClip rotorRotationFeedbackSound = null;

    public bool IsValid => reflecteur && rotors.Count > 0 && rotorRotationFeedbackSound;
    #endregion

    #region Unity Methods
    protected override void Awake()
    {
        base.Awake();
        if (!IsValid) return;
        OnRotateRotors += () =>
        {
            RotateRotors();
            EN_SoundsManager.Instance?.PlaySound(rotorRotationFeedbackSound);
        };
        OnLetterEncoded += (c) => EN_LightsManagerTemp.Instance?.SetLightPosition(c);
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
        OnLetterEncoded = null;
        OnRotateRotors = null;
    }
    #endregion

    #region Custom Methods
    public char Encode(char _letter)
    {
        if (!IsValid) return ' ';
        OnRotateRotors?.Invoke();
        _letter = _letter.ToString().ToUpper()[0];
        char _codedLetter = EncodeLetter(_letter);
        OnLetterEncoded?.Invoke(_codedLetter);
        return _codedLetter;
    }
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
    public void RotateRotors()
    {
        for (int i = 0; i < rotors.Count; i++)
        {
            bool _rotateNext = rotors[i].Rotation();
            if (!_rotateNext) return;
        }
    }
    public void ResetEnigma()
    {
        for (int i = 0; i < rotors.Count; i++)
            rotors[i].Reset();
        EN_LightsManagerTemp.Instance?.ResetLight();
    }
    #endregion
}
