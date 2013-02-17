using SafetyProgram.Models.DataModels;
using Microsoft.Practices.ServiceLocation;
using SafetyProgram.Data.CoshhFile;

namespace SafetyProgram.UserControls
{
    public class ViewModel : BaseINPC
    {
        private CoshhChemicalModel model;
        protected CurrentlyOpen currentlyOpen;

        private void baseCtor()
        {
            currentlyOpen = ServiceLocator.Current.GetInstance<CurrentlyOpen>();
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
