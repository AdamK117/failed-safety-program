using System.ComponentModel;

using SafetyProgram.Models.DataModels;

namespace SafetyProgram.UserControls.MainWindowControls.ClassLibrary
{
    public class ChemicalViewModel : BaseViewModel
    {
        private new CoshhChemicalModel model;
        private new IDocDataHolder<CoshhChemicalModel> CoshhDocDataObjectModel;

        public ChemicalViewModel(IDocDataHolder<CoshhChemicalModel> model)
            : base(model)
        {
            this.model = model.Data();
            this.CoshhDocDataObjectModel = model;
        }

        public override BaseElementModel GetModel()
        {
            return model;
        } 

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
