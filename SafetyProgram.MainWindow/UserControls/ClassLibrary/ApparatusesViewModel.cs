using SafetyProgram.Models.DataModels;
using SafetyProgram.Data;
using SafetyProgram.UserControls;

namespace SafetyProgram.MainWindow.UserControls.ClassLibrary
{
    public class ApparatusViewModel : BaseViewModel
    {
        private new IDocDataHolder<CoshhApparatusModel> model;

        public ApparatusViewModel(IDocDataHolder<CoshhApparatusModel> model) : base(model)
        {
            this.model = model;
        }

        public CoshhApparatusModel Model { get { return model.Data(); } }
    }
}
