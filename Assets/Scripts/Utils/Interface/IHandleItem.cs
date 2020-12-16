using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHandleItem<TID>
{ 
    TID ID { get; }
    void InitItem();
    void RemoveItem();
}
