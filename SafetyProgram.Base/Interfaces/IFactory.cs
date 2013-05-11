namespace SafetyProgram.Base.Interfaces
{
    public interface IFactory<Item, IoFormat>
    {
        Item CreateNew();
        Item Load(IoFormat data);
        IoFormat Store(Item item);
    }
}
