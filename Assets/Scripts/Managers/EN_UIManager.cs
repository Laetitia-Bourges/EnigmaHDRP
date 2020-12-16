using System.Diagnostics;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EN_UIManager : EN_Singleton<EN_UIManager>
{
    #region F/P
    [SerializeField] Button resetButton = null, quitButton = null, copyButton = null;
    [SerializeField] TMP_InputField msgToEncodeField = null;
    [SerializeField] TMP_Text encodedMsgTextArea = null, msgToEncodedTextArea = null;
    [SerializeField] AudioClip feedbackInputSound = null;
    string lastMessage = "";

    public bool IsUIValid => resetButton && quitButton && copyButton && msgToEncodeField && encodedMsgTextArea && msgToEncodedTextArea;
    public bool IsSoundValid => feedbackInputSound;
    #endregion

    #region Unity Methods
    protected override void Awake()
    {
        if (!IsUIValid) return;
        msgToEncodeField.onValueChanged.AddListener(VerifyMessage);
        resetButton.onClick.AddListener(ResetEnigma);
        quitButton.onClick.AddListener(Application.Quit);
        copyButton.onClick.AddListener(CopyMessage);
        if (IsSoundValid)
            msgToEncodeField.onValueChanged.AddListener((s) => EN_SoundsManager.Instance?.PlaySound(feedbackInputSound));
    }
    private void OnDestroy()
    {
        if (!IsUIValid) return;
        msgToEncodeField.onValueChanged.RemoveAllListeners();
        resetButton.onClick.RemoveAllListeners();
    }
    #endregion

    #region Custom Methods
    void VerifyMessage(string _message)
    {
        if (_message.Length <= lastMessage.Length) return;
        lastMessage = _message;
        encodedMsgTextArea.text += EN_Enigma.Instance.Encode(_message[_message.Length - 1]);
    }
    void ResetEnigma()
    {
        ResetUI();
        EN_Enigma.Instance?.ResetEnigma();
    }
    private void ResetUI()
    {
        msgToEncodeField.text = "";
        lastMessage = "";
        encodedMsgTextArea.text = "";
    }
    void CopyMessage()
    {
        string _path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "copy.txt");
        File.WriteAllText(_path, encodedMsgTextArea.text);
        Process.Start(_path);
    }
    #endregion
}
