using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;
using SafetyProgram.DocumentObjects.ChemicalTableNs.ContextMenus;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    /// <summary>
    /// Defines a ViewModel for a ChemicalTable
    /// </summary>
    public sealed class ChemicalTable : DocumentObject
    {
        /// <summary>
        /// Constructs an instance of the ChemicalTable DocObject
        /// </summary>
        /// <param name="appConfiguration">The applications configuration file (singleton, dependancy injected)</param>
        /// <param name="chemicals">The chemicals in this chemicaltable (may be empty)</param>
        /// <param name="header">The header for the chemicaltable</param>
        /// <param name="viewCtor">A constructor which generates a view compatiable with the chemicaltable as a viewmodel</param>
        internal ChemicalTable (
            IConfiguration appConfiguration, 
            ObservableCollection<ICoshhChemicalObject> chemicals, 
            string header,
            Func<ChemicalTable, UserControl> viewCtor
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

            if (viewCtor != null) view = viewCtor(this);
            else throw new ArgumentNullException();
        }

        private readonly IConfiguration appConfiguration;
        public IConfiguration AppConfiguration
        {
            get
            {
                return appConfiguration;
            }
        }

        private readonly UserControl view;
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

        private readonly IRibbonTabItem contextualTab;
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

        private readonly IContextMenu contextMenu;
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

        private readonly IChemicalTableCommands commands;
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
                PropertyChanged.Raise(this, "Header");
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

        public const string COM_IDENTITY = "CoshhChemicalModels";

        public override event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}