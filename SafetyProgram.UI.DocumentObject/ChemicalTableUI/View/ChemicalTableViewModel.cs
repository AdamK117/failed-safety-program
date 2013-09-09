using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Core.Models;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    /// <summary>
    /// Defines a standard implementation of a IChemicalTableViewModel.
    /// </summary>
    public sealed class ChemicalTableViewModel : IChemicalTableViewModel
    {
        /// <summary>
        /// Construct an instance of a ChemicalTableViewModel. A viewmodel for a chemical table view.
        /// </summary>
        /// <param name="chemicalTable">The underlying chemical table model to expose to the view.</param>
        /// <param name="chemicalTableCommands">Commands that act on the model.</param>
        public ChemicalTableViewModel(IChemicalTable chemicalTable,
            IChemicalTableCommands chemicalTableCommands)
        {
            this.chemicalTable = chemicalTable;
            this.chemicalTableCommands = chemicalTableCommands;

            this.selectedChemicals = new ObservableCollection<ICoshhChemical>();

            this.contextMenu = new ChemicalTableContextMenuView(
                new ChemicalTableContextMenuViewModel(
                    chemicalTableCommands));
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

        private readonly ContextMenu contextMenu;

        /// <summary>
        /// Get the context menu for the chemical table.
        /// </summary>
        public ContextMenu ContextMenu
        {
            get { return contextMenu; }
        }

        /// <summary>
        /// Get the chemicals in the chemical table.
        /// </summary>
        public ObservableCollection<ICoshhChemical> Chemicals
        {
            get { return chemicalTable.Content; }
        }

        private readonly ObservableCollection<ICoshhChemical> selectedChemicals;

        /// <summary>
        /// Get the chemicals selected in the chemical table.
        /// </summary>
        public ObservableCollection<ICoshhChemical> SelectedChemicals
        {
            get { return selectedChemicals; }
        }

        private IChemicalTableCommands chemicalTableCommands;

        /// <summary>
        /// Get the hotkeys exposed by the chemical table.
        /// </summary>
        public List<InputBinding> Hotkeys
        {
            get { return chemicalTableCommands.Hotkeys; }
        }

        /// <summary>
        /// Occurs when a property in this class changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
