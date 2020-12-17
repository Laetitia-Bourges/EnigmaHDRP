using System.Collections.Generic;

public interface IHandle<TID, TItem> where TItem : IHandleItem<TID>
{
    Dictionary<TID, TItem> Handles { get; }
    void Add(TItem _item);
    void Remove(TItem _item);
    TItem Get(TID _id);
    bool Exist(TID _id);
}
