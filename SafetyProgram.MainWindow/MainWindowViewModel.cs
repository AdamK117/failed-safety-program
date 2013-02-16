using System;
using System.Linq;
using System.Collections.ObjectModel;

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

            currentlyOpen.IsOpenChangedEvent += new ActiveCoshhData.isOpenChangedDelegate(currentlyOpen_IsOpenChangedEvent);
            currentlyOpen.SelectionChangedEvent += new ActiveCoshhData.selectionChangedDelegate(currentlyOpen_SelectionChangedEvent);

            currentlyOpen.Data.Chemicals.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler
                (
                    (sender, e) => genericCollectionChanged
                        (
                            sender,
                            e,
                            Chemicals,
                            (
                                (ICoshhObject<CoshhChemicalModel> x) => new ChemicalViewModel(x)
                            )
                        )
                );

            currentlyOpen.Data.Apparatuses.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler
                (
                    (sender, e) => genericCollectionChanged
                        (
                            sender,
                            e,
                            Apparatuses,
                            (
                                (ICoshhObject<CoshhApparatusModel> x) => new ApparatusViewModel(x)
                            )
                        )
                );

            currentlyOpen.Data.Processes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler
                (
                    (sender, e) => genericCollectionChanged
                        (
                            sender,
                            e,
                            Processes, 
                            (
                                (ICoshhObject<CoshhProcessModel> x) => new ProcessViewModel(x)
                            )
                        )
                );
        }

        

        #region ViewModel plumbing

        void genericCollectionChanged
            <ViewModel, ICoshhObjectModel>
            (object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e, ObservableCollection<ViewModel> viewModels, Func<ICoshhObjectModel, ViewModel> converter) 
            where ViewModel : BaseViewModel
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
                    foreach (ICoshhObjectModel model in e.NewItems)
                    {
                        viewModels.Insert(e.NewStartingIndex, converter(model));
                    }
                    break;

                case System.Collections.Specialized.NotifyCollectionChangedAction.Reset:
                    viewModels.Clear();
                    break;
            }
        }

        #endregion

        void currentlyOpen_IsOpenChangedEvent(bool isOpen)
        {
            RaisePropertyChanged("IsOpen");
        }
        public string IsOpen
        {
            get { return currentlyOpen.IsOpen() ? "Visible" : "Hidden"; }
        }

        #region Selection logic

        void currentlyOpen_SelectionChangedEvent(ICoshhObject<object> selection)
        {
            if (selection is ICoshhObject<CoshhChemicalModel>) { Selected = Chemicals.Single(x => x.GetICoshhObject() == selection as ICoshhObject<CoshhChemicalModel>); }
            else if (selection is ICoshhObject<CoshhApparatusModel>) { Selected = Chemicals.Single(x => x.GetICoshhObject() == selection as ICoshhObject<CoshhApparatusModel>); }
            else if (selection is ICoshhObject<CoshhProcessModel>) { Selected = Chemicals.Single(x => x.GetICoshhObject() == selection as ICoshhObject<CoshhProcessModel>); }
        }

        private BaseViewModel selectedViewModel;
        public BaseViewModel Selected
        {
            get { return selectedViewModel; }
            set
            {
                selectedViewModel = value;
                if (value != null) { currentlyOpen.Selected(value.GetICoshhObject()); }
                
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
            set { Selected = value; }
        }
        public ProcessViewModel ProcessSelection
        {
            get { return Selected as ProcessViewModel; }
            set { Selected = value; }
        }

        #endregion

        #region Data

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

        #endregion
    }
}
