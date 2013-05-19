using System;
using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class DeleteSelectedInvokedCom : IInvokedCommand
    {
        private readonly IChemicalTable table;
        private IEnumerable<ICoshhChemicalObject> deletedChemicals;

        public DeleteSelectedInvokedCom(IChemicalTable table)
        {
            if (table != null)
            {
                this.table = table;
            }
            else throw new ArgumentNullException();           
        }

        public void Execute()
        {
            deletedChemicals = new List<ICoshhChemicalObject>(table.SelectedChemicals);

            foreach (ICoshhChemicalObject chemical in deletedChemicals)
            {
                table.Chemicals.Remove(chemical);
            }
        }

        public void UnExecute()
        {
            foreach (ICoshhChemicalObject chemical in deletedChemicals)
            {
                table.Chemicals.Add(chemical);
            }
        }
    }
}
