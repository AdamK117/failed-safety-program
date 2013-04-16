using System;
using System.Collections.Generic;
using System.Windows;
using SafetyProgram.Models.DataModels;
using System.Runtime.InteropServices;

namespace SafetyProgram.DocObjects.ChemicalTable.Commands
{
    public class PasteChemicalsICommand : ChemicalTableCommandsBase
    {
        /// <summary>
        /// Creates a command that allows pasting of CoshhChemicalModel's into a ChemicalTable
        /// </summary>
        /// <param name="table"></param>
        public PasteChemicalsICommand(ChemicalTable table)
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
                if (Clipboard.ContainsData("CoshhChemicalModels"))
                {
                    try
                    {
                        object b = Clipboard.GetData("CoshhChemicalModels");
                        List<CoshhChemicalModel> a = (List<CoshhChemicalModel>)b;
                        a.ForEach(chemical => table.Chemicals.Add(chemical));
                    }
                    catch (COMException)
                    {
                        MessageBox.Show("Can't Access the Clipboard!");
                    }
                }
            }     
        }

    }
}
