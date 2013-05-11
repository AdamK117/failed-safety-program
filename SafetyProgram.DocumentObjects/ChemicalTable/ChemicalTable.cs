using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;
using SafetyProgram.DocumentObjects.ChemicalTableNs.ContextMenus;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    public sealed class ChemicalTable : DocumentObject
    {
        private readonly IChemicalTableCommands commands;
        private readonly IContextMenu contextMenu;
        private readonly IRibbonTabItem contextualTab;
        private readonly IConfiguration appConfiguration;
        private readonly UserControl view;

        internal ChemicalTable (
            IConfiguration appConfiguration, 
            ObservableCollection<ICoshhChemicalObject> chemicals, 
            string header,
            Func<ChemicalTable, ChemicalTableRibbonTab> ribbonCreator,
            Func<ChemicalTable, ChemicalTableView> viewCreator
            )
        {
            if (appConfiguration != null) this.appConfiguration = appConfiguration;
            else throw new ArgumentNullException();

            if (chemicals != null) this.chemicals = chemicals;
            else throw new ArgumentNullException();

            this.header = header;

            commands = new ChemicalTableCommands(this);
            contextMenu = new ChemicalTableContextMenu(this);
            contextualTab = new ChemicalTableRibbonTab(this);

            view = viewCreator(this);
        }

        /// <summary>
        /// Gets the ChemicalTable UserControl
        /// </summary>
        public override Control View
        {
            get 
            { 
                return view; 
            }
        }

        /// <summary>
        /// Gets the ribbon assosciated with a ChemicalTable.
        /// </summary>
        public override IRibbonTabItem RibbonTab
        {
            get 
            {
                return contextualTab; 
            }
        }

        /// <summary>
        /// Gets the ChemicalTable's context menu.
        /// </summary>
        public override IContextMenu ContextMenu
        {
            get 
            { 
                return contextMenu; 
            }
        }

        /// <summary>
        /// Gets the commands available to the ChemicalTable.
        /// </summary>
        public IChemicalTableCommands Commands
        {
            get 
            { 
                return commands; 
            }
        }

        private string header;
        /// <summary>
        /// Gets the header for the chemicalTable
        /// </summary>
        public string Header
        {
            get 
            { 
                return header; 
            }
            set
            {
                header = value;
                RaisePropertyChanged("Header");
            }
        }

        private readonly ObservableCollection<ICoshhChemicalObject> chemicals;
        /// <summary>
        /// Gets the chemicals in the ChemicalTable
        /// </summary>
        public ObservableCollection<ICoshhChemicalObject> Chemicals
        {
            get 
            { 
                return chemicals; 
            }
        }

        private readonly ObservableCollection<ICoshhChemicalObject> selectedChemicals = new ObservableCollection<ICoshhChemicalObject>();
        /// <summary>
        /// Gets/Sets the chemical selected in the table.
        /// </summary>
        public ObservableCollection<ICoshhChemicalObject> SelectedChemicals
        {
            get 
            {
                return selectedChemicals; 
            }
        }

        /// <summary>
        /// Deselects the chemicals in the ChemicalTable on top of base implementation.
        /// </summary>
        public override void DeSelect()
        {
            base.DeSelect();
            SelectedChemicals.Clear();
        }

        public override string Error
        {
            get 
            { 
                throw new System.NotImplementedException(); 
            }
        }

        public override string this[string columnName]
        {
            get 
            { 
                throw new System.NotImplementedException(); 
            }
        }
    }
}