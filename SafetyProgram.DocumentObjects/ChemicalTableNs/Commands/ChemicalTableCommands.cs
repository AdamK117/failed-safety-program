using System.Collections.Generic;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.GenericCommands;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class ChemicalTableCommands : IChemicalTableCommands
    {
        public ChemicalTableCommands(
            IChemicalTable table, 
            ICommandInvoker commandInvoker
            )
        {
            if (table != null && commandInvoker != null)
            {
                DeleteSelected = new DeleteSelectedICom<ICoshhChemicalObject>(table.SelectedChemicals, table.Chemicals, commandInvoker);
                InsertChemical = new InsertChemicalICom(table.Chemicals, commandInvoker);
                DeleteTable = new DeleteIDocumentObjectICom(table);
                CopySelected = new CopySelectedICom(table.SelectedChemicals);
                PasteChemicals = new PasteChemicalsICom(table.SelectedChemicals, commandInvoker);

                Hotkeys = setHotkeys();
            }            
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
        /// Gets an ICommand that flags the ChemicalTable for deletion.
        /// </summary>
        public ICommand DeleteTable 
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
