using SafetyProgram.Models.DataModels;
using Microsoft.Practices.ServiceLocation;

namespace SafetyProgram.UserControls
{
    public class ViewModel : BaseINPC
    {
        private CoshhChemicalModel model;

        private void baseCtor()
        {
            RaisePropertyChanged("Model");
        }

        public ViewModel()
        {            
            this.model = new CoshhChemicalModel();
            baseCtor();
        }

        public ViewModel(CoshhChemicalModel model)
        {
            this.model = model;
            baseCtor();
        }

        public CoshhChemicalModel Model
        {
            get { return model; }
            set { model = value; }
        }
    }
}
