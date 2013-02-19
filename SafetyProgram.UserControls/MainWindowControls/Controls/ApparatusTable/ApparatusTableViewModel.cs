using System.Collections.ObjectModel;
using SafetyProgram.Models.DataModels;
using SafetyProgram.UserControls.MainWindowControls.ClassLibrary;

namespace SafetyProgram.UserControls.MainWindowControls.ApparatusTable
{
    class ApparatusTableViewModel : BaseINPC
    {
        private ObservableCollection<IDocDataHolder<CoshhApparatusModel>> apparatuses;
        public ApparatusTableViewModel(ObservableCollection<IDocDataHolder<CoshhApparatusModel>> apparatuses)
        {
            this.apparatuses = apparatuses;
            this.apparatuses.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Apparatuses_CollectionChanged);
        }

        void Apparatuses_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MVVMPlumbers.genericCollectionChanged(sender, e, TableItems, (IDocDataHolder<CoshhApparatusModel> x) => new ApparatusViewModel(x));
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
