using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using SafetyProgram.MainWindow.UserControls.ClassLibrary;
using SafetyProgram.Data;
using Microsoft.Practices.ServiceLocation;
using SafetyProgram.Models.DataModels;
using SafetyProgram.Data.CoshhFile;
using SafetyProgram.UserControls;

namespace SafetyProgram.MainWindow.UserControls.ProcessesTable
{
    class ProcessesTableViewModel : BaseINPC
    {
        private CurrentlyOpen currentlyOpen;

        public ProcessesTableViewModel()
        {
            currentlyOpen = ServiceLocator.Current.GetInstance<CurrentlyOpen>();

            currentlyOpen.SelectionChangedEvent += new CurrentlyOpen.selectionChangedDelegate(currentlyOpen_SelectionChangedEvent);
            currentlyOpen.Data.Processes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Apparatuses_CollectionChanged);
        }

        void Apparatuses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MVVMPlumbers.genericCollectionChanged(sender, e, TableItems, (IDocDataHolder<CoshhProcessModel> x) => new ProcessViewModel(x));
        }

        void currentlyOpen_SelectionChangedEvent(IDocDataHolder<object> selection)
        {
            if (selection is IDocDataHolder<CoshhProcessModel>) { Selected = TableItems.Single(x => x.GetICoshhDocDataObject() == selection as IDocDataHolder<CoshhProcessModel>); }
        }

        private BaseViewModel selectedViewModel;
        public BaseViewModel Selected
        {
            get { return selectedViewModel as ProcessViewModel; }
            set
            {
                selectedViewModel = value;
                if (value != null) { currentlyOpen.Selected(value.GetICoshhDocDataObject()); }

                RaisePropertyChanged("Selected");
            }
        }

        private ObservableCollection<ProcessViewModel> tableItems = new ObservableCollection<ProcessViewModel>();
        public ObservableCollection<ProcessViewModel> TableItems
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
