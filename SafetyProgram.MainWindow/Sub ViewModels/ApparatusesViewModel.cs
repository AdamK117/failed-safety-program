using SafetyProgram.Models.DataModels;
using SafetyProgram.Data;

namespace SafetyProgram.MainWindow
{
    public class ApparatusViewModel : BaseViewModel
    {
        private new ICoshhObject<CoshhApparatusModel> model;

        public ApparatusViewModel(ICoshhObject<CoshhApparatusModel> model) : base(model)
        {
            this.model = model;
        }

        public CoshhApparatusModel Model { get { return model.Data(); } }
    }
}
