namespace SafetyProgram.MainWindow.Services
{
    public interface ICoshhDataService
    {
        bool New();

        bool Save();

        bool SaveAs();

        bool Load();        

        bool Close();
    }
}