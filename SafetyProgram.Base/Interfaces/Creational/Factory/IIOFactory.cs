namespace SafetyProgram.Base.Interfaces
{
    public interface IIOFactory<TItem, TIoFormat> : 
        IFactory<TItem>,
        IOutputFactory<TItem, TIoFormat>,
        IInputFactory<TIoFormat, TItem>
    { }
}
