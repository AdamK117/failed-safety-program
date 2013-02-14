using System.ComponentModel;

using SafetyProgram.Models.DataModels;

namespace SafetyProgram.MainWindow
{
    public class ChemicalViewModel : BaseViewModel
    {
        private new CoshhChemicalModel model;

        //Create viewmodel based on model, add handler to propertychanged handler to model
        public ChemicalViewModel(CoshhChemicalModel model) : base(model)
        {
            this.model = model;
        }

        //Expose model readonly
        public CoshhChemicalModel Model { get { return model; } }

        public float Value
        {
            get { return model.Value; }
            set
            {
                model.Value = value;
                RaisePropertyChanged("Value");
            }
        }

        public string Unit
        {
            get { return model.Unit; }
            set
            {
                model.Unit = value;
                RaisePropertyChanged("Unit");
            }
        }

        protected override void modelChanged(object sender, PropertyChangedEventArgs e)
        {
            base.modelChanged(sender, e);

            switch (e.PropertyName)
            {
                case "Value":
                case "Unit":
                    this.RaisePropertyChanged(e.PropertyName);
                    break;
            }
        }        
    }
}
