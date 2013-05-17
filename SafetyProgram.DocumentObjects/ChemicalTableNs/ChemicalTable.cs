using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    /// <summary>
    /// Defines a ViewModel for a ChemicalTable
    /// </summary>
    internal sealed class ChemicalTable : IChemicalTable
    {
        /// <summary>
        /// Constructs an instance of the ChemicalTable DocObject
        /// </summary>
        /// <param name="appConfiguration">The applications configuration file (singleton, dependancy injected)</param>
        /// <param name="chemicals">The chemicals in this chemicaltable (may be empty)</param>
        /// <param name="header">The header for the chemicaltable</param>
        /// <param name="viewConstructor">A constructor which generates a view compatiable with the chemicaltable as a viewmodel</param>
        public ChemicalTable (
            IConfiguration appConfiguration, 
            ObservableCollection<ICoshhChemicalObject> chemicals, 
            string header,
            Func<IChemicalTable, IChemicalTableCommands> commandsConstructor,
            Func<IChemicalTable, IContextMenu> contextMenuConstructor,
            Func<IChemicalTable, IRibbonTabItem> ribbonTabConstructor,
            Func<IChemicalTable, UserControl> viewConstructor
            )
        {
            if (
                appConfiguration != null &&
                chemicals != null &&
                commandsConstructor != null &&
                contextMenuConstructor != null &&
                ribbonTabConstructor != null &&
                viewConstructor != null
                )
            {
                this.appConfiguration = appConfiguration;
                this.chemicals = chemicals;
                this.header = header;
                this.commands = commandsConstructor(this);
                this.contextMenu = contextMenuConstructor(this);
                this.contextualTab = ribbonTabConstructor(this);
                this.view = viewConstructor(this);
            }
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
        public Control View
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
        public IRibbonTabItem RibbonTab
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
        public IContextMenu ContextMenu
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
        public void DeSelect()
        {
            //If the flag is actually changing
            if (selectedFlag == true)
            {
                selectedFlag = false;
                SelectedChanged.Raise(this, new GenericPropertyChangedEventArg<bool>(selectedFlag));
                PropertyChanged.Raise(this, "Selected");
            }
            SelectedChemicals.Clear();
        }

        public string Error
        {
            get 
            { 
                throw new System.NotImplementedException(); 
            }
        }

        public string this[string columnName]
        {
            get 
            { 
                throw new System.NotImplementedException(); 
            }
        }

        public const string COM_IDENTITY = "CoshhChemicalModels";

        public event PropertyChangedEventHandler PropertyChanged;

        private bool selectedFlag;
        public void Select()
        {
            //If the flag is actually changing
            if (selectedFlag == false)
            {
                selectedFlag = true;

                SelectedChanged.Raise(this, new GenericPropertyChangedEventArg<bool>(selectedFlag));
                PropertyChanged.Raise(this, "Selected");
            }
        }

        public bool Selected
        {
            get
            {
                return selectedFlag;
            }
        }

        public event EventHandler<GenericPropertyChangedEventArg<bool>> SelectedChanged;

        private bool removeFlag;
        public void FlagForRemoval()
        {
            if (removeFlag == false)
            {
                removeFlag = true;

                DeSelect();

                RemoveFlagChanged.Raise(this, new GenericPropertyChangedEventArg<bool>(removeFlag));
                PropertyChanged.Raise(this, "RemoveFlag");
            }
        }

        public bool RemoveFlag
        {
            get { return removeFlag; }
        }

        public event EventHandler<GenericPropertyChangedEventArg<bool>> RemoveFlagChanged;

        private bool editedFlag;
        public void FlagAsEdited()
        {
            if (editedFlag == false)
            {
                editedFlag = true;

                EditedFlagChanged.Raise(this, new GenericPropertyChangedEventArg<bool>(editedFlag));
                PropertyChanged.Raise(this, "EditedFlag");
            }
        }

        public bool EditedFlag
        {
            get 
            { 
                return editedFlag; 
            }
        }

        public event EventHandler<GenericPropertyChangedEventArg<bool>> EditedFlagChanged;
    }
}