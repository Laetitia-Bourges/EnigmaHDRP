using System.Collections.Generic;

public class EN_KeyboardAction : EN_Singleton<EN_KeyboardAction>, IHandle<char, EN_Key>
{
    #region Fields/Properties
    Dictionary<char, EN_Key> handle = new Dictionary<char, EN_Key>();
    char lastKey = ' ';

    public Dictionary<char, EN_Key> Handles => handle;
    #endregion

    #region Custom Methods
    /// <summary>
    /// Add a new key
    /// </summary>
    /// <param name="_item">item to add</param>
    public void Add(EN_Key _item)
    {
        if (!Exist(_item.ID))
            handle.Add(_item.ID, _item);
    }
    /// <summary>
    /// Verify if an id already exist or not
    /// </summary>
    /// <param name="_id">id to verify</param>
    /// <returns>true if the id already exist, else false</returns>
    public bool Exist(char _id)
    {
        if (handle.ContainsKey(_id)) return true;
        return false;
    }
    /// <summary>
    /// Get the item corresponding at an id
    /// </summary>
    /// <param name="_id">id of the researched item</param>
    /// <returns>the item corresponding at the index (null if the index doesn't exist)</returns>
    public EN_Key Get(char _id)
    {
        if (!Exist(_id)) return null;
        return handle[_id];
    }
    /// <summary>
    /// Remove an item from the manager
    /// </summary>
    /// <param name="_item"></param>
    public void Remove(EN_Key _item)
    {
        if (Exist(_item.ID))
            handle.Remove(_item.ID);
    }
    /// <summary>
    /// Reset all the keys
    /// </summary>
    public void ResetKeyBoard()
    {
        if (char.IsWhiteSpace(lastKey)) return;
        Get(lastKey).Disable();
    }
    /// <summary>
    /// Change the current pushed key
    /// </summary>
    /// <param name="_id"></param>
    public void UpdateKeyBoard(char _id)
    {
        if (!char.IsWhiteSpace(lastKey))
            Get(lastKey).Disable();
        Get(_id).Enable();
        lastKey = _id;
    }
    #endregion
}