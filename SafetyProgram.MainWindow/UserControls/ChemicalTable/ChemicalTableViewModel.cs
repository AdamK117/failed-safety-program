using System.Linq;
using SafetyProgram.Data;
using Microsoft.Practices.ServiceLocation;
using System.Collections.ObjectModel;

using SafetyProgram.Models.DataModels;
using SafetyProgram.MainWindow.UserControls.ClassLibrary;
using SafetyProgram.Data.CoshhFile;
using SafetyProgram.UserControls;

namespace SafetyProgram.MainWindow.UserControls.ChemicalTable
{
    public class ChemicalTableViewModel : BaseINPC
    {
        private CurrentlyOpen currentlyOpen;

        public ChemicalTableViewModel()
        {
            currentlyOpen = ServiceLocator.Current.GetInstance<CurrentlyOpen>();
            currentlyOpen.SelectionChangedEvent += new CurrentlyOpen.selectionChangedDelegate(currentlyOpen_SelectionChangedEvent);
            currentlyOpen.Data.Chemicals.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Chemicals_CollectionChanged);
        }

        void Chemicals_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MVVMPlumbers.genericCollectionChanged(sender, e, TableItems, (IDocDataHolder<CoshhChemicalModel> x) => new ChemicalViewModel(x));
        }

        void currentlyOpen_SelectionChangedEvent(IDocDataHolder<object> selection)
        {
            if (selection is IDocDataHolder<CoshhChemicalModel>) { Selected = TableItems.Single(x => x.GetICoshhDocDataObject() == selection as IDocDataHolder<CoshhChemicalModel>); }
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

        private ObservableCollection<ChemicalViewModel> tableItems = new ObservableCollection<ChemicalViewModel>();
        public ObservableCollection<ChemicalViewModel> TableItems
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
