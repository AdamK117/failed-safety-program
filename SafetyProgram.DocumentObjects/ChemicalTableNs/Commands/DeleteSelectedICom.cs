using System;
using System.Windows.Forms;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class DeleteSelectedICom : ICommand
    {
        private readonly IChemicalTable table;
        private readonly ICommandInvoker commandInvoker;

        public DeleteSelectedICom(IChemicalTable table, ICommandInvoker commandInvoker) 
        {
            if (table != null && commandInvoker != null)
            {
                this.table = table;
                this.commandInvoker = commandInvoker;
            }
            else throw new ArgumentNullException();

            table.SelectedChemicals.CollectionChanged += (sender, args) => CanExecuteChanged.Raise(this);  
        }

        /// <summary>
        /// Can only execute if there are chemicals selected
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return (table.SelectedChemicals.Count) == 0 ? false : true;
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
                        var invokedCommand = new DeleteSelectedInvokedCom(table);
                        commandInvoker.InvokeCommand(invokedCommand);
                        break;

                    default:
                        break;
                }                
            }
        }

        public event System.EventHandler CanExecuteChanged;
    }
}