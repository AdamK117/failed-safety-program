using System.Collections.ObjectModel;
using Microsoft.Practices.ServiceLocation;
using SafetyProgram.Models.DataModels;
using SafetyProgram.UserControls.MainWindowControls.ClassLibrary;

namespace SafetyProgram.UserControls.MainWindowControls.ProcessesTable
{
    class ProcessesTableViewModel : BaseINPC
    {
        private ObservableCollection<IDocDataHolder<CoshhProcessModel>> processes;
        public ProcessesTableViewModel(ObservableCollection<IDocDataHolder<CoshhProcessModel>> processes)
        {
            this.processes = processes;
            this.processes.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Apparatuses_CollectionChanged);
        }

        void Apparatuses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MVVMPlumbers.genericCollectionChanged(sender, e, TableItems, (IDocDataHolder<CoshhProcessModel> x) => new ProcessViewModel(x));
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
