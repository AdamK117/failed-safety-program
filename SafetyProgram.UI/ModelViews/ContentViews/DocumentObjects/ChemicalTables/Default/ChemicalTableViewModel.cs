using System.Collections.ObjectModel;
using System.ComponentModel;
using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.ModelViews.ContentViews.DocumentObjects.ChemicalTables.Default
{
    /// <summary>
    /// Defines a standard implementation of a IChemicalTableViewModel.
    /// </summary>
    internal sealed class ChemicalTableViewModel : IChemicalTableViewModel
    {
        /// <summary>
        /// Construct an instance of a ChemicalTableViewModel. A viewmodel for a chemical table view.
        /// </summary>
        /// <param name="chemicalTable">The underlying chemical table model to expose to the view.</param>
        /// <param name="chemicalTableCommands">Commands that act on the model.</param>
        public ChemicalTableViewModel(IChemicalTable chemicalTable)
        {
            this.chemicalTable = chemicalTable;

            this.Chemicals = chemicalTable.Content;

            chemicalTable.HeaderChanged +=
                (s, e) => PropertyChanged.Raise(this, "Header");
        }

        private IChemicalTable chemicalTable;

        /// <summary>
        /// Get or set the header of the chemical table.
        /// </summary>
        public string Header
        {
            get { return chemicalTable.Header; }
            set
            {
                chemicalTable.Header = value;
                PropertyChanged.Raise(this, "Header");
            }
        }

        /// <summary>
        /// Get the chemicals in the chemical table.
        /// </summary>
        public ObservableCollection<
            ICoshhChemical> Chemicals { get; private set; }

        /// <summary>
        /// Occurs when a property in this class changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
