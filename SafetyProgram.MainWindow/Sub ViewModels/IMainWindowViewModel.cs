using SafetyProgram.Models.DataModels;
using SafetyProgram.Data;
namespace SafetyProgram.MainWindow
{
    interface IMainWindowViewModel
    {
        BaseElementModel GetModel();
        ICoshhObject<BaseElementModel> GetICoshhObject();
    }
}
