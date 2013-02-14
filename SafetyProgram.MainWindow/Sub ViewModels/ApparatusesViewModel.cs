using SafetyProgram.Models.DataModels;

namespace SafetyProgram.MainWindow
{
    public class ApparatusViewModel : BaseViewModel
    {
        private new ApparatusModel model;

        public ApparatusViewModel(ApparatusModel model) : base(model)
        {
            this.model = model;
        }

        public ApparatusModel Model { get { return model; } }
    }
}
