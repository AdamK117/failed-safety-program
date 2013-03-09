using System.Collections.ObjectModel;

using SafetyProgram.Models.DataModels;
using SafetyProgram.UserControls.MainWindowControls.ClassLibrary;

namespace SafetyProgram.UserControls.MainWindowControls.ChemicalTable
{
    public class ChemicalTableViewModel : BaseINPC
    {
        //Underlying model data
        private ObservableCollection<CoshhDocDataObject<CoshhChemicalModel>> chemicals;

        //Constructed ViewModel data
        private ObservableCollection<ChemicalViewModel> tableItems = new ObservableCollection<ChemicalViewModel>();

        /// <summary>
        /// Constructs a ViewModel for the chemical table. Takes an IEnumerable of CoshhDocDataObjects.
        /// </summary>
        /// <param name="chemicals"></param>
        public ChemicalTableViewModel(ObservableCollection<CoshhDocDataObject<CoshhChemicalModel>> chemicals)
        {
            this.chemicals = chemicals;
            foreach (CoshhDocDataObject<CoshhChemicalModel> chemical in chemicals)
            {
                tableItems.Add(new ChemicalViewModel(chemical));
            }
            this.chemicals.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Chemicals_CollectionChanged);
        }

        /// <summary>
        /// Event that fires when the underlying data collection is changed (e.g. an item is deleted from the collection).
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Chemicals_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MVVMPlumbers.genericCollectionChanged(sender, e, TableItems, (CoshhDocDataObject<CoshhChemicalModel> x) => new ChemicalViewModel(x));
        }

        /// <summary>
        /// Exposes the constructed viewmodels. The view (Chemical Table) will digest this data in order to display the desired properties.
        /// </summary>
        public ObservableCollection<ChemicalViewModel> TableItems
        {
            get { return tableItems; }
            set
            {
                tableItems = value;
                RaisePropertyChanged("TableItems");
            }
        }

        public ChemicalViewModel Selected
        {
            set { }
        }
    }
}
