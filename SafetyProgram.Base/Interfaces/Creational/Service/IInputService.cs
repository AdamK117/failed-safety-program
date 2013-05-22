namespace SafetyProgram.Base.Interfaces
{
    public interface IInputService<in T>
    {
        void Save(T data);

        bool CanSave(T data);

        void SaveAs(T data);

        bool CanSaveAs(T data);

        void Disconnect();
    }
}
