using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace SafetyProgram.Base.GenericCommands
{
    public sealed class DeleteSelectedICom<T> : ICommand
    {
        private readonly ObservableCollection<T> selection;
        private readonly IList<T> items;
        private readonly ICommandInvoker commandInvoker;

        public DeleteSelectedICom(ObservableCollection<T> selection, 
            IList<T> items, 
            ICommandInvoker commandInvoker) 
        {
            Helpers.NullCheck(selection, items, commandInvoker);

            this.selection = selection;
            this.items = items;
            this.commandInvoker = commandInvoker;

            this.selection.CollectionChanged += 
                (sender, args) => CanExecuteChanged.Raise(this);
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
                //DialogResult userPrompt = 
                //    MessageBox.Show(Locale.SystemMessages.DeleteSelectedMessage, 
                //    Locale.SystemMessages.DeleteSelectedMessage, 
                //    MessageBoxButtons.YesNo);

                //if (userPrompt == DialogResult.Yes)
                //{
                //    var invokedCommand = new DeleteSelectedInvokedCom<T>(selection, items);
                //    commandInvoker.InvokeCommand(invokedCommand);
                //}              
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}