using SafetyProgram.Models.DataModels;
using SafetyProgram.UserControls.MainWindowControls.ClassLibrary;

namespace SafetyProgram.UserControls.MainWindowControls.ApparatusTable
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
