using System;
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
        }

        public string Header
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public System.Collections.ObjectModel.ObservableCollection<ICoshhChemical> Chemicals
        {
            get { throw new NotImplementedException(); }
        }

        public System.Windows.Controls.Control View
        {
            get { throw new NotImplementedException(); }
        }

        public Fluent.RibbonTabItem ContextualTab
        {
            get { throw new NotImplementedException(); }
        }
    }
}
