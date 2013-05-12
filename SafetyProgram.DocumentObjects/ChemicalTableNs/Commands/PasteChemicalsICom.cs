using System.Windows.Input;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class PasteChemicalsICom : ICommand
    {
        private readonly ChemicalTable data;

        /// <summary>
        /// Creates a command that allows pasting of CoshhChemicalModel's into a ChemicalTable
        /// </summary>
        /// <param name="table"></param>
        public PasteChemicalsICom(ChemicalTable table)
        {
            this.data = table;
        }

        /// <summary>
        /// Can always execute
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Attempts to paste CoshhChemicalModels into the ChemicalTable
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="System.OutOfMemoryException">Thrown when a non-serializable object is put on the clipboard</exception>
        /// <exception cref="System.InvalidCastException"></exception>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                data.Chemicals.TryPaste();
            }     
        }

        public event System.EventHandler CanExecuteChanged;
    }
}
