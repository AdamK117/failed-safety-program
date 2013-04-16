using System.Runtime.InteropServices;
using System.Windows;

namespace SafetyProgram.DocObjects.ChemicalTable.Commands
{
    public class CopySelectedICommand : ChemicalTableCommandsBase
    {
        public CopySelectedICommand(ChemicalTable table)
            : base(table)
        {
            table.SelectedChemicals.CollectionChanged += (sender, args) => RaiseCanExecuteChanged();
        }

        /// <summary>
        /// Can only execute if there is currently a selection in the ChemicalTable to copy.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return table.SelectedChemicals.Count == 0 ? false : true;
        }

        /// <summary>
        /// Copies the selected CoshhChemicalModel(s) to the clipboard.
        /// </summary>
        /// <param name="parameter">Unused paramater.</param>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                try
                {
                    ChemicalTableComObject cObj = new ChemicalTableComObject(table.SelectedChemicals, "CoshhChemicalModels");

                    Clipboard.SetDataObject(cObj.GetDataObject(), true);
                }
                catch (COMException)
                {
                    MessageBox.Show("Can't Access the Clipboard!");
                }
            }                
        }
    }
}
