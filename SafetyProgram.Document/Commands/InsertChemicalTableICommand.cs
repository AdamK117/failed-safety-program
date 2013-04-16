using SafetyProgram.DocObjects.ChemicalTable;
using System;

namespace SafetyProgram.Document.Commands
{
    public class InsertChemicalTableICommand : DocumentCommandsBase
    {
        public InsertChemicalTableICommand(CoshhDocument document) : base(document) 
        { }

        /// <summary>
        /// Can always execute.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public override bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Inserts a ChemicalTable into the CoshhDocument
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="NotSupportedException">Thrown if Execute is called but CanExecute == false</exception>
        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                document.Body.Add(new ChemicalTable());
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)");
        }
    }
}
