using SafetyProgram.Models.DataModels;
using SafetyProgram.Data;

namespace SafetyProgram.MainWindow
{
    public class ProcessViewModel : BaseViewModel
    {
        private new ProcessModel model;
        private new ICoshhObject<CoshhProcessModel> coshhObjectModel;

        public ProcessViewModel(ICoshhObject<CoshhProcessModel> model)
            : base(model)
        {
            this.model = model.Data();
            this.coshhObjectModel = model;
        }

        public ProcessModel Model { get { return model; } }
    }
}
