using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows;
using SafetyProgram.ModelObjects;
using SafetyProgram.Base;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    public sealed class PasteChemicalsICom : ExtendedICommand<ChemicalTable>
    {
        /// <summary>
        /// Creates a command that allows pasting of CoshhChemicalModel's into a ChemicalTable
        /// </summary>
        /// <param name="table"></param>
        internal PasteChemicalsICom(ChemicalTable table)
            : base(table)
        { }

        /// <summary>
        /// Can always execute
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Attempts to paste CoshhChemicalModels into the ChemicalTable
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="System.OutOfMemoryException">Thrown when a non-serializable object is put on the clipboard</exception>
        /// <exception cref="System.InvalidCastException"></exception>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                data.Chemicals.TryPaste();
            }     
        }
    }
}
