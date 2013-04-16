using System.Collections.Generic;
using System.Windows.Forms;

using SafetyProgram.Models.DataModels;

namespace SafetyProgram.DocObjects.ChemicalTable.Commands
{
    public class DeleteSelectedICommand : ChemicalTableCommandsBase
    {
        public DeleteSelectedICommand(ChemicalTable table) : base(table) 
        {
            //Monitor the ChemicalTable's selections. This command won't work if nothing is selected.
            table.SelectedChemicals.CollectionChanged += (sender, args) => RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Can only execute if there are chemicals selected
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return table.SelectedChemicals.Count == 0 ? false : true;
        }

        /// <summary>
        /// Deletes the selected chemicals in the ChemicalTable.
        /// </summary>
        /// <param name="parameter">Unused paramater.</param>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                DialogResult userPrompt = MessageBox.Show("Are you sure you want to delete these chemical(s)?", "Confirm Delete.", MessageBoxButtons.YesNo);

                switch (userPrompt)
                {
                    case DialogResult.Yes:
                        //Create a cache of the selection.
                        List<CoshhChemicalModel> selection = new List<CoshhChemicalModel>(table.SelectedChemicals);

                        //Remove the selection from the chemicals in the table
                        selection.ForEach(x => table.Chemicals.Remove(x));
                        break;

                    default:
                        break;
                }
            }                        
        }
    }
}