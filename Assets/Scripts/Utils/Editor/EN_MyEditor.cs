using UnityEngine;
using UnityEditor;

public abstract class EN_MyEditor<T> : Editor where T : MonoBehaviour
{
    protected T eTarget = default(T);
    protected virtual void OnEnable()
    {
        eTarget = (T)target;
    }
}
