using System.ComponentModel;

using SafetyProgram.Models.DataModels;
using SafetyProgram.UserControls.MainWindowControls.ClassLibrary;

namespace SafetyProgram.UserControls.MainWindowControls.ChemicalTable
{
    public class ChemicalViewModel : BaseViewModel
    {
        private new CoshhChemicalModel model;
        private bool selected;

        public ChemicalViewModel(CoshhChemicalModel model)
            : base(model)
        {
            this.model = model;
        }

        public override BaseElementModel GetModel()
        {
            return model;
        }

        #region Data

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

        #endregion

        /// <summary>
        /// Shows if the ViewModel is currently selected (will highlight it in the View).
        /// </summary>
        /// <returns></returns>
        public bool Selected()
        {
            return selected;
        }

        /// <summary>
        /// Sets the Chemical View Model as selected, also sets the underlying data as selected.
        /// </summary>
        /// <param name="selected"></param>
        /// <returns></returns>
        public bool Selected(bool selected)
        {
            this.selected = selected;
            RaisePropertyChanged("Selected");
            return selected;
        }

        /// <summary>
        /// Event that fires if data in the underlying model changes (i.e. from another view/control)
        /// </summary>
        /// <param name="sender">The model that has changed</param>
        /// <param name="e">The property changed (from RaisePropertyChanged("PropertyName");)</param>
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
