using System;
using System.Linq;
using System.Collections.ObjectModel;
using System.ComponentModel;

using Microsoft.Practices.ServiceLocation;

using SafetyProgram.Models.DataModels;
using SafetyProgram.Data;
using System.Collections.Generic;

namespace SafetyProgram.MainWindow
{
    public class MainWindowViewModel : BaseINPC
    {
        private ActiveCoshhData currentlyOpen;

        public MainWindowViewModel()
        {     
            currentlyOpen = ServiceLocator.Current.GetInstance<ActiveCoshhData>();
            currentlyOpen.PropertyChanged += filePropertyChanged;
            PopulateData();
        }

        void filePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsOpen":
                    PopulateData();
                    RaisePropertyChanged("IsOpen");
                    break;

                case "Selected":
                    CurrentSelectionChanged();
                    break;
            }
        }

        #region ViewModel plumbing

        public void PopulateData()
        {
            currentlyOpen.Data.Chemicals.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Chemicals_CollectionChanged);
            Chemicals = populateViewModels(Chemicals, currentlyOpen.Data.Chemicals, x => new ChemicalViewModel(x));
            
            currentlyOpen.Data.Apparatuses.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Apparatuses_CollectionChanged);
            Apparatuses = populateViewModels(Apparatuses, currentlyOpen.Data.Apparatuses, x => new ApparatusViewModel(x));

            currentlyOpen.Data.Processes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Processes_CollectionChanged);
            Processes = populateViewModels(Processes, currentlyOpen.Data.Processes, x => new ProcessViewModel(x));
        }

        ObservableCollection<ViewModel> populateViewModels<ViewModel, Model>(ObservableCollection<ViewModel> viewModels, ObservableCollection<Model> models, Func<Model,ViewModel> converter)
        {
            foreach (Model model in models)
            {
                viewModels.Add(converter(model));
            }
            return viewModels;
        }

        void Processes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            genericCollectionChanged<ProcessViewModel, CoshhProcessModel>
                (
                    sender,
                    e,
                    Processes,
                    x => new ProcessViewModel(x)
                );
        }

        void Apparatuses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            genericCollectionChanged<ApparatusViewModel, CoshhApparatusModel>
                (
                    sender,
                    e,
                    Apparatuses,
                    x => new ApparatusViewModel(x)
                );
        }

        void Chemicals_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            genericCollectionChanged<ChemicalViewModel,CoshhChemicalModel>
                (
                    sender, 
                    e, 
                    Chemicals,
                    x => new ChemicalViewModel(x)
                );
        }

        void genericCollectionChanged<ViewModel, Model>(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e, ObservableCollection<ViewModel> viewModels, Func<Model, ViewModel> converter) where ViewModel : BaseViewModel
        {
            switch (e.Action)
            {
                case System.Collections.Specialized.NotifyCollectionChangedAction.Remove:
                    foreach (ViewModel vm in viewModels)
                    {
                        if (e.OldItems.Contains(vm.GetModel()))
                        {
                            viewModels.Remove(vm);                            
                            break;
                        }
                    }
                    break;
                case System.Collections.Specialized.NotifyCollectionChangedAction.Add:
                    foreach (Model model in e.NewItems)
                    {
                        viewModels.Insert(e.NewStartingIndex, converter(model));
                    }
                    break;
            }
        }

        #endregion

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

        private ObservableCollection<ChemicalViewModel> chemicals = new ObservableCollection<ChemicalViewModel>();
        public ObservableCollection<ChemicalViewModel> Chemicals 
        {
            get
            {
                return chemicals;
            }
            set
            {
                chemicals = value;
                RaisePropertyChanged("Chemicals");
            }
        }

        private ObservableCollection<ApparatusViewModel> apparatuses = new ObservableCollection<ApparatusViewModel>();
        public ObservableCollection<ApparatusViewModel> Apparatuses
        {
            get { return apparatuses; }
            set
            {
                apparatuses = value;
                RaisePropertyChanged("Apparatuses");
            }
        }

        private ObservableCollection<ProcessViewModel> processes = new ObservableCollection<ProcessViewModel>();
        public ObservableCollection<ProcessViewModel> Processes
        {
            get { return processes; }
            set
            {
                processes = value;
                RaisePropertyChanged("Processes");
            }
        }

        public string AdditionalComments
        {
            get { return currentlyOpen.Data.AdditionalComments; }
            set
            { 
                currentlyOpen.Data.AdditionalComments = value;
                RaisePropertyChanged("AdditionalComments");
            }
        }
    }
}
