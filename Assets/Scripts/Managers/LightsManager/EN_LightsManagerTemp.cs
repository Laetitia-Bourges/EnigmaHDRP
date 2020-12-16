using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EN_LightsManagerTemp : EN_Singleton<EN_LightsManagerTemp>
{
    #region F/P
    [SerializeField] Light lightFeedback = null;
    [SerializeField] Vector3 currentCursorPosition = Vector3.zero;
    [SerializeField] string characterID = "";
    [SerializeField] List<string> idList = new List<string>();
    [SerializeField] List<Vector3> positionList = new List<Vector3>();
    Dictionary<char, Vector3> handles = new Dictionary<char, Vector3>();

    public Dictionary<char, Vector3> Handles => handles;
    public bool IsValid => lightFeedback;
    #endregion

    private void Start()
    {
        GenerateDictionnary();
    }
    private void OnDestroy()
    {
        RemoveManager();
    }

    #region Custom Methods
    void GenerateDictionnary()
    {
        for (int i = 0; i < idList.Count; i++)
            if (!Exist(idList[i][0]))
                handles.Add(idList[i][0], positionList[i]);
    }

    public void Add(string _id, Vector3 _position)
    {
        idList.Add(_id);
        positionList.Add(_position);
    }
    public void SetLightPosition(char _id)
    {
        if (!IsValid) return;
        lightFeedback.transform.position = handles[_id];
    }
    public bool Exist(char _id)
    {
        if (handles.ContainsKey(_id)) return true;
        return false;
    }
    public void Remove(int _index)
    {
        idList.RemoveAt(_index);
        positionList.RemoveAt(_index);
    }
    void RemoveManager()
    {
        handles = new Dictionary<char, Vector3>();
    }
    public void ResetLight() => lightFeedback.transform.position = Vector3.zero;
    #endregion
}
