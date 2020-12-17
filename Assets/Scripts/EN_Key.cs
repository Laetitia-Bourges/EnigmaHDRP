using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EN_Key : MonoBehaviour, IHandleItem<char>
{
    [SerializeField] char id = ' ';
    Vector3 initialPos = Vector3.zero;
    public char ID => id;
    public bool IsValid => !char.IsWhiteSpace(id);

    void Start() => InitItem();
    void OnDestroy() => RemoveItem();

    public void InitItem()
    {
        if (!IsValid) return;
        id = id.ToString().ToUpper()[0];
        initialPos = transform.position;
        EN_KeyboardAction.Instance?.Add(this);
    }

    public void RemoveItem()
    {
        EN_KeyboardAction.Instance?.Remove(this);
    }

    public void Enable()
    {
        transform.position = initialPos - Vector3.up * .5f;
    }

    public void Disable()
    {
        transform.position = initialPos;
    }
}
