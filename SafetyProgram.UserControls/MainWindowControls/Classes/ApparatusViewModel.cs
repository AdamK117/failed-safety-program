using SafetyProgram.Models.DataModels;

namespace SafetyProgram.UserControls.MainWindowControls.ClassLibrary
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
