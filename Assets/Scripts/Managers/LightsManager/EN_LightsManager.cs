using System.Collections.Generic;
using UnityEngine;

public class EN_LightsManager : EN_Singleton<EN_LightsManager>, IHandle<char, EN_PanelLight>
{
    #region F/P
    [SerializeField] Light lightFeedback = null;
    Dictionary<char, EN_PanelLight> handles = new Dictionary<char, EN_PanelLight>();

    public Dictionary<char, EN_PanelLight> Handles => handles;
    public bool IsValid => lightFeedback;
    #endregion

    #region Custom Methods
    public void Add(EN_PanelLight _item)
    {
        if (Exist(_item.ID)) return;
        handles.Add(_item.ID, _item);
    }
    public void SetLightPosition(char _id)
    {
        if (!IsValid) return;
        lightFeedback.transform.position = Get(_id).LightPosition;
    }
    public bool Exist(char _id)
    {
        if (handles.ContainsKey(_id)) return true;
        return false;
    }
    public EN_PanelLight Get(char _id)
    {
        if (!Exist(_id)) return null;
        return handles[_id];
    }
    public void Remove(EN_PanelLight _item)
    {
        if (!Exist(_item.ID)) return;
        handles.Remove(_item.ID);
    }
    public void ResetLight() => lightFeedback.transform.position = Vector3.zero;
    #endregion
}
