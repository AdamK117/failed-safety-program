using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class DeleteSelectedICom : ICommand
    {
        private readonly IChemicalTable table;

        public DeleteSelectedICom(IChemicalTable table) 
        {
            this.table = table;
            //Monitor the ChemicalTable's selections. This command won't work if nothing is selected.
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
                        //Create a cache of the selection.
                        List<ICoshhChemicalObject> selection = new List<ICoshhChemicalObject>(table.SelectedChemicals);

                        //Remove the selection from the chemicals in the table
                        selection.ForEach(x => table.Chemicals.Remove(x));
                        break;

                    default:
                        break;
                }
            }
        }

        public event System.EventHandler CanExecuteChanged;
    }
}