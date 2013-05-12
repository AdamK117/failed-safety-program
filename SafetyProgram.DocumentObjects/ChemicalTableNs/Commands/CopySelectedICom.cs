using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using SafetyProgram.Base;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class CopySelectedICom : ICommand
    {
        private readonly ChemicalTable data;

        public CopySelectedICom(ChemicalTable table)
        {
            this.data = table;
            table.SelectedChemicals.CollectionChanged += (sender, args) => CanExecuteChanged.Raise(this);
        }

        /// <summary>
        /// Can only execute if there is currently a selection in the ChemicalTable to copy.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return data.SelectedChemicals.Count == 0 ? false : true;
        }

        /// <summary>
        /// Copies the selected CoshhChemicalModel(s) to the clipboard.
        /// </summary>
        /// <param name="parameter">Unused paramater.</param>
        public void Execute(object parameter)
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

        public event System.EventHandler CanExecuteChanged;
    }
}
