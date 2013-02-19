using SafetyProgram.Models.DataModels;
using SafetyProgram.Data;
using SafetyProgram.UserControls;

namespace SafetyProgram.MainWindow.UserControls.ClassLibrary
{
    public class ProcessViewModel : BaseViewModel
    {
        private new ProcessModel model;
        private new IDocDataHolder<CoshhProcessModel> CoshhDocDataObjectModel;

        public ProcessViewModel(IDocDataHolder<CoshhProcessModel> model)
            : base(model)
        {
            this.model = model.Data();
            this.CoshhDocDataObjectModel = model;
        }

        public ProcessModel Model { get { return model; } }
    }
}
