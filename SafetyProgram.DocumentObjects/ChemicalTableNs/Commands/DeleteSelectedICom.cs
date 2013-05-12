using System.Collections.Generic;
using System.Windows.Forms;

using SafetyProgram.ModelObjects;
using SafetyProgram.Base;
using System.Windows.Input;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    public sealed class DeleteSelectedICom : ICommand
    {
        private readonly ChemicalTable data;

        internal DeleteSelectedICom(ChemicalTable table) 
        {
            this.data = table;
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
            return data.SelectedChemicals.Count == 0 ? false : true;
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
                        List<ICoshhChemicalObject> selection = new List<ICoshhChemicalObject>(data.SelectedChemicals);

                        //Remove the selection from the chemicals in the table
                        selection.ForEach(x => data.Chemicals.Remove(x));
                        break;

                    default:
                        break;
                }
            }
        }

        public event System.EventHandler CanExecuteChanged;
    }
}