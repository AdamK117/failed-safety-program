using System.Linq;
using System.Collections.ObjectModel;
using SafetyProgram.MainWindow.UserControls.ClassLibrary;
using SafetyProgram.Data;
using Microsoft.Practices.ServiceLocation;
using SafetyProgram.Models.DataModels;
using SafetyProgram.Data.CoshhFile;
using SafetyProgram.UserControls;

namespace SafetyProgram.MainWindow.UserControls.ApparatusTable
{
    class ApparatusTableViewModel : BaseINPC
    {
        private CurrentlyOpen currentlyOpen;

        public ApparatusTableViewModel()
        {
            currentlyOpen = ServiceLocator.Current.GetInstance<CurrentlyOpen>();
            currentlyOpen.SelectionChangedEvent += new CurrentlyOpen.selectionChangedDelegate(currentlyOpen_SelectionChangedEvent);
            currentlyOpen.Data.Apparatuses.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Apparatuses_CollectionChanged);
        }

        void Apparatuses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MVVMPlumbers.genericCollectionChanged(sender, e, TableItems, (IDocDataHolder<CoshhApparatusModel> x) => new ApparatusViewModel(x));
        }

        void currentlyOpen_SelectionChangedEvent(IDocDataHolder<object> selection)
        {
            if (selection is IDocDataHolder<CoshhApparatusModel>) { Selected = TableItems.Single(x => x.GetICoshhDocDataObject() == selection as IDocDataHolder<CoshhApparatusModel>); }
        }

        private BaseViewModel selectedViewModel;
        public BaseViewModel Selected
        {
            get { return selectedViewModel as ChemicalViewModel; }
            set
            {
                selectedViewModel = value;
                if (value != null) { currentlyOpen.Selected(value.GetICoshhDocDataObject()); }

                RaisePropertyChanged("Selected");
            }
        }

        private ObservableCollection<ApparatusViewModel> tableItems = new ObservableCollection<ApparatusViewModel>();
        public ObservableCollection<ApparatusViewModel> TableItems
        {
            get { return tableItems; }
            set
            {
                tableItems = value;
                RaisePropertyChanged("TableItems");
            }
        }
    }
}
