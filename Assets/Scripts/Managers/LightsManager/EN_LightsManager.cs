using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EN_LightsManager : EN_Singleton<EN_LightsManager>
{
    #region F/P
    [SerializeField] Light lightFeedback = null;
    #region Editor
    [SerializeField] Vector3 currentCursorPosition = Vector3.zero;
    [SerializeField] string characterID = "";
    [SerializeField] List<string> idList = new List<string>();
    [SerializeField] List<Vector3> positionList = new List<Vector3>();
    #endregion
    Dictionary<char, Vector3> handles = new Dictionary<char, Vector3>();

    public Dictionary<char, Vector3> Handles => handles;
    public bool IsValid => lightFeedback;
    public string CharacterID => characterID; // juste pour enlever le warning qui dit que characterID n'est pas utilise (alors qu'il est utlisé dans l'editeur)
    #endregion

    #region UnityMethods
    private void Start() => GenerateDictionnary();
    private void OnDestroy() => RemoveManager();
    #endregion

    #region Custom Methods
    /// <summary>
    /// Generate the handle dictionary with lists of the editor
    /// </summary>
    void GenerateDictionnary()
    {
        for (int i = 0; i < idList.Count; i++)
            if (!Exist(idList[i][0]))
                handles.Add(idList[i][0], positionList[i]);
    }
    /// <summary>
    /// Set the manager's light position
    /// </summary>
    /// <param name="_id">id of the position to move the light</param>
    public void SetLightPosition(char _id)
    {
        if (!IsValid) return;
        lightFeedback.transform.position = handles[_id];
    }/// <summary>
     /// Add a new position at the editor lists
     /// </summary>
     /// <param name="_id">id to add</param>
     /// <param name="_position">position to add</param>
    public void Add(string _id, Vector3 _position)
    {
        idList.Add(_id);
        positionList.Add(_position);
    }
    /// <summary>
    /// Says if the id already exist in the handle dictionnary or not
    /// </summary>
    /// <param name="_id">id to verify</param>
    /// <returns>true if the id exist, else false</returns>
    public bool Exist(char _id)
    {
        if (handles.ContainsKey(_id)) return true;
        return false;
    }
    /// <summary>
    /// remove id and position at the index of the editor's lists
    /// </summary>
    /// <param name="_index">index to remove</param>
    public void Remove(int _index)
    {
        idList.RemoveAt(_index);
        positionList.RemoveAt(_index);
    }
    /// <summary>
    /// Remove all the Pair in the manager dictionary
    /// </summary>
    void RemoveManager() => handles = new Dictionary<char, Vector3>();
    /// <summary>
    /// Reset the light position
    /// </summary>
    public void ResetLight() => lightFeedback.transform.position = Vector3.zero;
    #endregion
}
