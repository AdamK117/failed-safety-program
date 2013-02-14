using System.Linq;
using SafetyProgram.Models.DataModels;
using System.ComponentModel;

namespace SafetyProgram.MainWindow
{
    public abstract class BaseViewModel : BaseINPC, IMainWindowViewModel
    {
        protected BaseElementModel model;

        public BaseViewModel(BaseElementModel model) 
        { 
            this.model = model;
            this.model.PropertyChanged += new PropertyChangedEventHandler(modelChanged);
        }

        public virtual object GetModel() { return model; }

        public string Name
        {
            get { return model.Name; }
            set
            {
                model.Name = value;
                RaisePropertyChanged("Name");
            }
        }

        public string Hazards
        {
            get
            {
                if (model.Hazards != null)
                {
                    string hazards = "";
                    foreach (HazardModel hazard in model.Hazards)
                    {
                        hazards += hazard.Hazard + ", ";
                    }
                    hazards = hazards.Substring(0, hazards.Length - 2);
                    return hazards;
                }
                return "No hazards found";
            }
            set
            {
                //BUG: Need to split with space after comma
                model.Hazards = value.Split(',').ToList().ConvertAll(x => new HazardModel(x));
                RaisePropertyChanged("Hazards");
            }
        }

        protected virtual void modelChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Name":
                case "Hazards":
                    this.RaisePropertyChanged(e.PropertyName);
                    break;
            }
        }
    }
}
