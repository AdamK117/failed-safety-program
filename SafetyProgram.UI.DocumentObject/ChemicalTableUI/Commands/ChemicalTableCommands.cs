using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Base.GenericCommands;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    internal sealed class ChemicalTableCommands : IChemicalTableCommands
    {
        public ChemicalTableCommands(ObservableCollection<ICoshhChemical> selectedChemicals,
            IList<ICoshhChemical> chemicals,
            ICommandInvoker commandInvoker)
        {
            Helpers.NullCheck(selectedChemicals, chemicals, commandInvoker);

            DeleteSelected = new DeleteSelectedICom<ICoshhChemical>(selectedChemicals, chemicals, commandInvoker);
            InsertChemical = new InsertChemicalICom(chemicals, commandInvoker);
            CopySelected = new CopySelectedICom(selectedChemicals);
            PasteChemicals = new PasteChemicalsICom(chemicals, commandInvoker);

            Hotkeys = setHotkeys();   
        }

        private List<InputBinding> setHotkeys()
        {
            return new List<InputBinding>()
            {
                //DEL: Delete selected
                new InputBinding(
                    DeleteSelected,
                    new KeyGesture(Key.Delete)
                ),
                //CTRL+C: Copy selected
                new InputBinding(
                    CopySelected,
                    new KeyGesture(Key.C, ModifierKeys.Control)
                ),
                //CTRL+V: Paste into selected
                new InputBinding(
                    PasteChemicals,
                    new KeyGesture(Key.V, ModifierKeys.Control)
                )
            };
        }

        /// <summary>
        /// Gets an ICommand that deletes chemicals selected within the ChemicalTable.
        /// </summary>
        public ICommand DeleteSelected 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Gets an ICommand that copies the selected chemicals within the ChemicalTable
        /// </summary>
        public ICommand CopySelected 
        { 
            get; 
            private set; 
        }

        /// <summary>
        /// Gets an ICommand that attempts to paste chemicals into the ChemicalTable.
        /// </summary>
        public ICommand PasteChemicals 
        { 
            get; 
            private set; 
        }

        public ICommand InsertChemical
        {
            get;
            private set;
        }

        /// <summary>
        /// Gets the hotkeys associated with the ChemicalTable.
        /// </summary>
        public List<InputBinding> Hotkeys 
        { 
            get; 
            private set; 
        }        
    }
}
