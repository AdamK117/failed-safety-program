using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

using SafetyProgram.MainWindow.Document.Controls.ChemicalTable.Commands;
using SafetyProgram.MainWindow.Document.Controls.ChemicalTable.ContextMenus;
using SafetyProgram.MainWindow.Document.Controls.ChemicalTable.Ribbon;
using SafetyProgram.Models.DataModels;

namespace SafetyProgram.MainWindow.Document.Controls.ChemicalTable
{
    public class ChemicalTable : DocObject
    {
        private readonly ObservableCollection<CoshhChemicalModel> chemicals;
        private readonly ChemicalTableCommandsHolder commands;
        private readonly DocObjectContextMenu contextMenu;
        private readonly DocObjectRibbon ribbon;
        private readonly UserControl view;

        private CoshhChemicalModel selectedChemical;
        private bool isSelected;
        private bool removeFlag;

        /// <summary>
        /// Constructs a ChemicalTable DocObject containing no data (blank table).
        /// </summary>
        /// <param name="parent">The document in which the chemical table resides.</param>
        public ChemicalTable()
        {
            chemicals = new ObservableCollection<CoshhChemicalModel>();

            commands = new ChemicalTableCommandsHolder(this);
            contextMenu = new ChemicalTableContextMenu(this);
            ribbon = new ChemicalTableRibbon(this);

            view = new ChemicalTableView(this);
            view.InputBindings.AddRange(commands.Hotkeys);
        }

        /// <summary>
        /// Constructs a new ChemicalTable DocObject populated with data.
        /// </summary>
        /// <param name="data">Chemicals to populate into the table</param>
        /// <param name="parent">The document in which the chemical table resides</param>
        public ChemicalTable(IEnumerable<CoshhChemicalModel> data) : this()
        {
            foreach (CoshhChemicalModel model in data)
            {
                chemicals.Add(model);
            }
        }

        /// <summary>
        /// Gets the ChemicalTable UserControl
        /// </summary>
        public override UserControl View
        {
            get { return view; }
        }

        /// <summary>
        /// Gets the ribbon assosciated with a ChemicalTable.
        /// </summary>
        public override DocObjectRibbon Ribbon
        {
            get { return ribbon; }
        }

        /// <summary>
        /// Gets the ChemicalTable's context menu.
        /// </summary>
        public override DocObjectContextMenu ContextMenu
        {
            get { return contextMenu; }
        }

        /// <summary>
        /// Gets the commands available to the ChemicalTable.
        /// </summary>
        public ChemicalTableCommandsHolder Commands
        {
            get { return commands; }
        }

        /// <summary>
        /// Gets the chemicals in the ChemicalTable
        /// </summary>
        public ObservableCollection<CoshhChemicalModel> Chemicals
        {
            get { return chemicals; }
        }

        /// <summary>
        /// Gets/Sets the chemical selected in the table.
        /// </summary>
        public CoshhChemicalModel SelectedChemical
        {
            get { return selectedChemical; }
            set
            {
                selectedChemical = value;
                RaisePropertyChanged("SelectedChemical");                
            }
        }

        /// <summary>
        /// Gets/Sets if the ChemicalTable is selected.
        /// </summary>
        public override bool Selected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    if (SelectedChanged != null)
                    {
                        SelectedChanged(this, isSelected);
                    }
                    RaisePropertyChanged("Selected");
                }
            }
        }
        public override event isSelectedChangedDelegate SelectedChanged;

        /// <summary>
        /// Flags the ChemicalTable for removal.
        /// </summary>
        /// <returns></returns>
        public override bool RemoveFlag
        {
            get { return removeFlag; }
            set 
            {
                removeFlag = value;
                Selected = false;                
                if (RemoveFlagChanged != null)
                {
                    RemoveFlagChanged(this, true);
                }
            }            
        }
        public override event removeFlagDelegate RemoveFlagChanged;

        public override bool CanRemove()
        {
            return true;
        }

        public override bool CanEdit()
        {
            throw new NotImplementedException();
        }
    }
}