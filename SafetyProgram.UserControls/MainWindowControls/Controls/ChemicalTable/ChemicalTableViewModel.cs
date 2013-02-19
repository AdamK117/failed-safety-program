using System.Collections.ObjectModel;

using SafetyProgram.Models.DataModels;
using SafetyProgram.UserControls.MainWindowControls.ClassLibrary;

namespace SafetyProgram.UserControls.MainWindowControls.ChemicalTable
{
    public class ChemicalTableViewModel : BaseINPC
    {
        private ObservableCollection<CoshhDocDataObject<CoshhChemicalModel>> chemicals;

        public ChemicalTableViewModel(ObservableCollection<CoshhDocDataObject<CoshhChemicalModel>> chemicals)
        {
            this.chemicals = chemicals;
            foreach (CoshhDocDataObject<CoshhChemicalModel> chemical in chemicals)
            {
                tableItems.Add(new ChemicalViewModel(chemical));
            }
            this.chemicals.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Chemicals_CollectionChanged);
        }

        void Chemicals_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MVVMPlumbers.genericCollectionChanged(sender, e, TableItems, (CoshhDocDataObject<CoshhChemicalModel> x) => new ChemicalViewModel(x));
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
