using UnityEngine;

public class EN_Key : MonoBehaviour, IHandleItem<char>
{
    #region F/P
    [SerializeField] char id = ' ';
    Vector3 initialPos = Vector3.zero;
    public char ID => id;
    public bool IsValid => !char.IsWhiteSpace(id);
    #endregion

    #region Unity Methods
    void Start() => InitItem();
    void OnDestroy() => RemoveItem();
    #endregion

    #region Custom Methods
    /// <summary>
    /// Init The Keybord key
    /// </summary>
    public void InitItem()
    {
        if (!IsValid) return;
        id = id.ToString().ToUpper()[0];
        initialPos = transform.position;
        EN_KeyboardAction.Instance?.Add(this);
    }
    /// <summary>
    /// Remove the keyord key
    /// </summary>
    public void RemoveItem() => EN_KeyboardAction.Instance?.Remove(this);
    /// <summary>
    /// Push the key
    /// </summary>
    public void Enable() => transform.position = initialPos - Vector3.up * .5f;
    /// <summary>
    /// Go up the key
    /// </summary>
    public void Disable() => transform.position = initialPos;
    #endregion
}
