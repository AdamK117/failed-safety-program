using System;
using SafetyProgram.DocumentObjectUi.ChemicalTableNs.Commands;
using SafetyProgram.Models;

namespace SafetyProgram.DocumentObjectUi.ChemicalTableNs.Ribbon
{
    public sealed class ChemicalTableRibbonViewModel : IChemicalTableRibbonViewModel
    {
        public ChemicalTableRibbonViewModel(IChemicalTableCommands commands,
            IConfiguration configuration)
        { }

        public string Search
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

        public System.Collections.ObjectModel.ObservableCollection<Models.IChemical> SearchResult
        {
            get { throw new NotImplementedException(); }
        }

        public Commands.IChemicalTableCommands Commands
        {
            get { throw new NotImplementedException(); }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
