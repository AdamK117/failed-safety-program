namespace SafetyProgram.Base.Interfaces
{
    public interface IFactoryIO<TItem, TIoFormat> : 
        IFactory<TItem>,
        ILoader<TItem, TIoFormat>,
        ISaver<TIoFormat, TItem>
    { }
}
