using System.Collections.ObjectModel;

using SafetyProgram.Models.DataModels;
using SafetyProgram.UserControls.MainWindowControls.ClassLibrary;
using System.Windows;

namespace SafetyProgram.UserControls.MainWindowControls.ChemicalTable
{
    public class ChemicalTableViewModel : BaseINPC
    {
        //Underlying model data
        private ObservableCollection<CoshhChemicalModel> chemicals;

        //Constructed ViewModel data
        private ObservableCollection<ChemicalViewModel> tableItems = new ObservableCollection<ChemicalViewModel>();

        /// <summary>
        /// Constructs a ViewModel for the chemical table. Takes an IEnumerable of CoshhDocDataObjects containing chemicals.
        /// </summary>
        /// <param name="chemicals"></param>
        public ChemicalTableViewModel(ObservableCollection<CoshhChemicalModel> chemicals)
        {
            this.chemicals = chemicals;

            //Add the models to a ViewModel
            foreach (CoshhChemicalModel chemical in chemicals)
            {
                tableItems.Add(new ChemicalViewModel(chemical));
            }

            //this.chemicals.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(Chemicals_CollectionChanged);
        }

        /// <summary>
        /// Event that fires when the underlying data collection is changed (e.g. an item is deleted from the collection). Uses MVVM Plumber to plumb the models into the ViewModel.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /*void Chemicals_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            MVVMPlumbers.genericCollectionChanged(sender, e, TableItems, (CoshhChemicalModel x) => new ChemicalViewModel(x));
        }*/

        /// <summary>
        /// Items in the ChemicalTable view.
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

        /// <summary>
        /// Item selected within the ChemicalTable
        /// </summary>
        public ChemicalViewModel Selected
        {
            set
            {
                value.Selected(true);
            }
        }
    }
}
