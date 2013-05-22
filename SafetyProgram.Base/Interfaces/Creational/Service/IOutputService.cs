namespace SafetyProgram.Base.Interfaces
{
    public interface IOutputService<out T>
    {
        T Load();

        bool CanLoad();

        void Disconnect();
    }
}
