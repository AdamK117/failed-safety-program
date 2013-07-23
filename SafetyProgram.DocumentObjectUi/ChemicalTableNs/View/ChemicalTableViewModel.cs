using System;
using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Models;

namespace SafetyProgram.DocumentObjectUi.ChemicalTableNs.View
{
    public sealed class ChemicalTableViewModel : IChemicalTableViewModel
    {
        public ChemicalTableViewModel(IChemicalTable chemicalTable,
            IConfiguration configuration,
            ICommandInvoker commandInvoker)
        {
            //monitor the kernel etc.
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

        public System.Windows.Controls.ContextMenu ContextMenu
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.ObjectModel.ObservableCollection<Models.ICoshhChemical> Chemicals
        {
            get { throw new NotImplementedException(); }
        }

        public System.Collections.ObjectModel.ObservableCollection<Models.ICoshhChemical> SelectedChemicals
        {
            get { throw new NotImplementedException(); }
        }

        public List<System.Windows.Input.InputBinding> Hotkeys
        {
            get { throw new NotImplementedException(); }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
    }
}
