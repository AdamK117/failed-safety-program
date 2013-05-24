using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Forms;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class DeleteSelectedICom<T> : ICommand
    {
        private readonly ObservableCollection<T> selection;
        private readonly ICollection<T> items;
        private readonly ICommandInvoker commandInvoker;

        public DeleteSelectedICom(ObservableCollection<T> selection, 
            ICollection<T> items, 
            ICommandInvoker commandInvoker) 
        {
            if (Helpers.NullCheck(selection, items, commandInvoker))
            {
                this.selection = selection;
                this.items = items;
                this.commandInvoker = commandInvoker;

                this.selection.CollectionChanged += (sender, args) => CanExecuteChanged.Raise(this); 
            }                     
        }

        /// <summary>
        /// Can only execute if there are chemicals selected
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return (selection.Count == 0) ? false : true;
        }

        /// <summary>
        /// Deletes the selected chemicals in the ChemicalTable.
        /// </summary>
        /// <param name="parameter">Unused paramater.</param>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                DialogResult userPrompt = MessageBox.Show("Are you sure you want to delete these chemical(s)?", "Confirm Delete.", MessageBoxButtons.YesNo);

                switch (userPrompt)
                {
                    case DialogResult.Yes:
                        var invokedCommand = new DeleteSelectedInvokedCom<T>(selection, items);
                        commandInvoker.InvokeCommand(invokedCommand);
                        break;

                    default:
                        break;
                }                
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}