namespace SafetyProgram.Base.Interfaces
{
    public interface IService<T>
    {
        T New();

        bool CanNew();

        T Load();

        bool CanLoad();

        void Save(T data);

        bool CanSave(T data);

        void SaveAs(T data);

        bool CanSaveAs(T data);

        void Disconnect();
    }
}
