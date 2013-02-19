using System.Linq;
using SafetyProgram.Models.DataModels;
using System.ComponentModel;
using SafetyProgram.Data;
using SafetyProgram.UserControls;

namespace SafetyProgram.MainWindow.UserControls.ClassLibrary
{
    public abstract class BaseViewModel : BaseINPC
    {
        protected BaseElementModel model;
        protected IDocDataHolder<BaseElementModel> CoshhDocDataObjectModel;

        public BaseViewModel(IDocDataHolder<BaseElementModel> iCoshhDocDataObject) 
        {
            this.model = iCoshhDocDataObject.Data();
            this.CoshhDocDataObjectModel = iCoshhDocDataObject;

            this.model.PropertyChanged += new PropertyChangedEventHandler(modelChanged);
            this.model.Hazards.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Hazards_CollectionChanged);
        }

        void Hazards_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            RaisePropertyChanged("Hazards");
        }

        public virtual BaseElementModel GetModel() { return model; }

        public virtual IDocDataHolder<BaseElementModel> GetICoshhDocDataObject() { return CoshhDocDataObjectModel; }

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
