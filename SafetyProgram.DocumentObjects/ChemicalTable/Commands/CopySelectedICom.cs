using System.Runtime.InteropServices;
using System.Windows;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs;
using SafetyProgram.ModelObjects;
namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class CopySelectedICom : ExtendedICommand<ChemicalTable>
    {
        public CopySelectedICom(ChemicalTable table)
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
            return data.SelectedChemicals.Count == 0 ? false : true;
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
                    data.SelectedChemicals.TryCopy();
                }
                catch (COMException)
                {
                    MessageBox.Show("Can't Access the Clipboard!");
                    throw;
                }
            }                
        }
    }
}
