using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    public sealed class ChemicalTableUiController : IChemicalTableUiController
    {
        public ChemicalTableUiController(IChemicalTable chemicalTable, 
            IConfiguration configuration, 
            ICommandInvoker commandInvoker)
        {
            this.chemicals = chemicalTable.Chemicals;
            this.header = chemicalTable.Header;

            this.view = new ChemicalTableView(
                new ChemicalTableViewModel(
        }

        private string header;

        public string Header
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
            }
        }

        private readonly ObservableCollection<ICoshhChemical> chemicals;

        public ObservableCollection<ICoshhChemical> Chemicals
        {
            get { return chemicals; }
        }

        private readonly Control view;

        public Control View
        {
            get { return view; }
        }

        private readonly RibbonTabItem contextualTab;

        public Fluent.RibbonTabItem ContextualTab
        {
            get { return contextualTab; }
        }
    }
}
