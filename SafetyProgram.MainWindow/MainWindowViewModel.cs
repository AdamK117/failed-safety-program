using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;

using Microsoft.Practices.ServiceLocation;

using SafetyProgram.Models.DataModels;
using SafetyProgram.Data;

namespace SafetyProgram.MainWindow
{
    public class MainWindowViewModel : BaseINPC
    {
        private ActiveCoshhData currentlyOpen;

        public MainWindowViewModel()
        {     
            currentlyOpen = ServiceLocator.Current.GetInstance<ActiveCoshhData>();
            currentlyOpen.PropertyChanged += filePropertyChanged;
        }

        void filePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Data":
                    PopulateData();
                    break;

                case "IsOpen":
                    RaisePropertyChanged("IsOpen");
                    break;

                case "Selected":
                    CurrentSelectionChanged();
                    break;

                case "FileChanged":
                    this.RaisePropertyChanged(e.PropertyName);
                    break;
            }
        }

        public void PopulateData()
        {
            RaisePropertyChanged("Title");

            Chemicals = new ObservableCollection<ChemicalViewModel>();
            currentlyOpen.Data.Chemicals.ForEach(x => Chemicals.Add(new ChemicalViewModel(x)));
            RaisePropertyChanged("Chemicals");

            Apparatuses = new ObservableCollection<ApparatusViewModel>();
            currentlyOpen.Data.Apparatuses.ForEach(x => Apparatuses.Add(new ApparatusViewModel(x)));
            RaisePropertyChanged("Apparatuses");

            Processes = new ObservableCollection<ProcessViewModel>();
            currentlyOpen.Data.Processes.ForEach(x => Processes.Add(new ProcessViewModel(x)));
            RaisePropertyChanged("Processes");

            RaisePropertyChanged("AdditionalComments");
        }

        public string IsOpen
        {
            get { return currentlyOpen.IsOpen() ? "Visible" : "Hidden"; }
        }

        #region Selection logic
        
        public void CurrentSelectionChanged()
        {
            if (currentlyOpen.Selected is CoshhChemicalModel)
            {
                selectedViewModel = Chemicals.Single(i => i.Model == currentlyOpen.Selected as CoshhChemicalModel);
            }
            else if (currentlyOpen.Selected is CoshhApparatusModel)
            {
                selectedViewModel = Apparatuses.Single(i => i.Model == currentlyOpen.Selected as CoshhApparatusModel);
            }
            else if (currentlyOpen.Selected is CoshhProcessModel)
            {
                selectedViewModel = Processes.Single(i => i.Model == currentlyOpen.Selected as CoshhProcessModel);
            }
            else if (currentlyOpen.Selected == null) { selectedViewModel = null; }

            RaisePropertyChanged("ChemicalSelection");
            RaisePropertyChanged("ApparatusSelection");
            RaisePropertyChanged("ProcessSelection");
        }

        private object selectedViewModel;
        public object Selected
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;

                if (value != null)
                {
                    IMainWindowViewModel a = (IMainWindowViewModel)value;
                    currentlyOpen.Selected = a.GetModel();
                }
                
                RaisePropertyChanged("ChemicalSelection");
                RaisePropertyChanged("ApparatusSelection");
                RaisePropertyChanged("ProcessSelection");
            }
        }

        public ChemicalViewModel ChemicalSelection
        {
            get { return Selected as ChemicalViewModel; }
            set { Selected = value; }
        }
        public ApparatusViewModel ApparatusSelection
        {
            get { return Selected as ApparatusViewModel; }
            set { Selected = value as object; }
        }
        public ProcessViewModel ProcessSelection
        {
            get { return Selected as ProcessViewModel; }
            set { Selected = value as object; }
        }

        #endregion

        //Exposes ViewModels to the View
        public string Title
        {
            get { return currentlyOpen.Data.Title; }
            set
            {
                currentlyOpen.Data.Title = value;
                RaisePropertyChanged("Title");
            }
        }

        public ObservableCollection<ChemicalViewModel> Chemicals { get; set; }
        public ObservableCollection<ApparatusViewModel> Apparatuses { get; set; }
        public ObservableCollection<ProcessViewModel> Processes { get; set; }

        public string AdditionalComments
        {
            get
            {
                if (String.IsNullOrWhiteSpace(currentlyOpen.Data.AdditionalComments)) { return "No additional comments"; }
                return currentlyOpen.Data.AdditionalComments;
            }
            set
            { 
                currentlyOpen.Data.AdditionalComments = value;
                RaisePropertyChanged("AdditionalComments");
            }
        }
    }
}
