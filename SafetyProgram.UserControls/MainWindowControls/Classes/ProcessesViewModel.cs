using SafetyProgram.Models.DataModels;

namespace SafetyProgram.UserControls.MainWindowControls.ClassLibrary
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
