namespace SafetyProgram.Base.Interfaces
{
    public interface IIOService<T> :
        IInputService<T>,
        IOutputService<T>
    {
        T New();

        bool CanNew();

        new void Disconnect();
    }
}
