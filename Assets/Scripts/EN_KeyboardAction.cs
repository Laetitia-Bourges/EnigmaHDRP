using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EN_KeyboardAction : EN_Singleton<EN_KeyboardAction>, IHandle<char, EN_Key>
{
    Dictionary<char, EN_Key> handle = new Dictionary<char, EN_Key>();
    public Dictionary<char, EN_Key> Handles => throw new System.NotImplementedException();

    char lastKey = ' ';

    public void Add(EN_Key _item)
    {
        if (!Exist(_item.ID))
            handle.Add(_item.ID, _item);
    }

    public bool Exist(char _id)
    {
        if (handle.ContainsKey(_id)) return true;
        return false;
    }

    public EN_Key Get(char _id)
    {
        if (!Exist(_id)) return null;
        return handle[_id];
    }

    public void Remove(EN_Key _item)
    {
        if (Exist(_item.ID))
            handle.Remove(_item.ID);
    }

    public void ResetKeyBoard()
    {
        if (char.IsWhiteSpace(lastKey)) return;
        Get(lastKey).Disable();
    }

    public void UpdateKeyBoard(char _id)
    {
        if (!char.IsWhiteSpace(lastKey))
            Get(lastKey).Disable();
        Get(_id).Enable();
        lastKey = _id;
    }
}