using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EN_UIManager : EN_Singleton<EN_UIManager>
{
    #region F/P
    [SerializeField] Button resetButton = null, quitButton = null, copyButton = null, applyButton = null;
    [SerializeField] TMP_InputField msgToEncodeField = null;
    [SerializeField] TMP_Text encodedMsgTextArea = null, msgToEncodedTextArea = null;
    [SerializeField] TMP_Text rotor1Config = null, rotor2Config = null, rotor3Config = null;
    [SerializeField] AudioClip feedbackInputSound = null;

    public bool IsUIValid => resetButton && quitButton && copyButton && applyButton && msgToEncodeField && 
        encodedMsgTextArea && msgToEncodedTextArea && rotor1Config && rotor2Config && rotor3Config;
    public bool IsSoundValid => feedbackInputSound;
    #endregion

    #region Unity Methods
    protected override void Awake() => InitUI();
    private void Start() => InitRotorConfigText();
    private void OnDestroy() => RemoveUI();
    #endregion

    #region Custom Methods
    #region Init/Remove
    /// <summary>
    /// Init UI Listener
    /// </summary>
    void InitUI()
    {
        if (!IsUIValid) return;
        msgToEncodeField.onValueChanged.AddListener(VerifyMessage);
        resetButton.onClick.AddListener(ResetEnigma);
        quitButton.onClick.AddListener(Application.Quit);
        copyButton.onClick.AddListener(CopyMessage);
        applyButton.onClick.AddListener(ChangeConfiguration);
        if (IsSoundValid)
            msgToEncodeField.onValueChanged.AddListener((s) => EN_SoundsManager.Instance?.PlaySound(feedbackInputSound));
    }
    /// <summary>
    /// Remove UI Listeners
    /// </summary>
    void RemoveUI()
    {
        if (!IsUIValid) return;
        msgToEncodeField.onValueChanged.RemoveAllListeners();
        resetButton.onClick.RemoveAllListeners();
        quitButton.onClick.RemoveAllListeners();
        copyButton.onClick.RemoveAllListeners();
        applyButton.onClick.RemoveAllListeners();
    }
    /// <summary>
    /// Init Rotors configuration UI text
    /// </summary>
    void InitRotorConfigText()
    {
        if (!IsUIValid) return;
        List<EN_Rotor> rotors = EN_Enigma.Instance.Rotors;
        rotor1Config.text += rotors[0].StartConfig.ToString();
        rotor2Config.text += rotors[1].StartConfig.ToString();
        rotor3Config.text += rotors[2].StartConfig.ToString();
    }
    #endregion
    #region Button fonctions
    /// <summary>
    /// Verify the input message and decode the last letter
    /// </summary>
    /// <param name="_message"></param>
    void VerifyMessage(string _message)
    {
        if (string.IsNullOrEmpty(_message)) return;
        char _lastChar = _message[_message.Length - 1];
        if(char.IsLetter(_lastChar))
            encodedMsgTextArea.text += EN_Enigma.Instance.Encode(_lastChar);
    }
    /// <summary>
    /// Copy the crypted message and open it in a txt file
    /// </summary>
    void CopyMessage()
    {
        string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "copy.txt");
        File.WriteAllText(_path, encodedMsgTextArea.text);
        Process.Start(_path);
    }
    /// <summary>
    /// Set the new start configuration of the rotors
    /// </summary>
    void ChangeConfiguration()
    {
        EN_Enigma.Instance?.ChangeRotorConfiguration(rotor1Config.text.ToUpper()[0], rotor2Config.text.ToUpper()[0], rotor3Config.text.ToUpper()[0]);
        ResetEnigma();
    }
    #endregion
    #region Reset
    /// <summary>
    /// Reset the machine
    /// </summary>
    void ResetEnigma()
    {
        ResetUI();
        EN_Enigma.Instance?.ResetEnigma();
        EN_LightsManager.Instance?.ResetLight();
        EN_KeyboardAction.Instance?.ResetKeyBoard();
        msgToEncodeField.Select();
    }
    /// <summary>
    /// Reset UI Text
    /// </summary>
    private void ResetUI()
    {
        msgToEncodeField.text = "";
        encodedMsgTextArea.text = "";
    }
    #endregion
    #endregion
}
