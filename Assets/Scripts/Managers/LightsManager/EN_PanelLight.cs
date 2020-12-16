using UnityEngine;

public class EN_PanelLight : MonoBehaviour, IHandleItem<char>
{
    [SerializeField] string id = "";
    public char ID => id[0];
    public Vector3 LightPosition => transform.position;

    void Start() => InitItem();
    void OnDestroy() => RemoveItem();

    public void InitItem() => EN_LightsManager.Instance?.Add(this);
    public void RemoveItem() => EN_LightsManager.Instance?.Remove(this);

    void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(LightPosition, .2f);
    }
}
