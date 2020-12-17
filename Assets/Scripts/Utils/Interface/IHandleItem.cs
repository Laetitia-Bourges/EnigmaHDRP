public interface IHandleItem<TID>
{ 
    TID ID { get; }
    void InitItem();
    void RemoveItem();
    void Enable();
    void Disable();

}
