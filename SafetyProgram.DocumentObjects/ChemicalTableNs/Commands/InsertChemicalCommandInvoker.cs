using System;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class InsertChemicalInvokedCom : IInvokedCommand
    {
        private readonly IChemicalTable table;
        private readonly ICoshhChemicalObject addedChemical;

        public InsertChemicalInvokedCom(IChemicalTable table, ICoshhChemicalObject addedChemical)
        {
            this.table = table;
            this.addedChemical = addedChemical;
        }

        public void Execute()
        {
            table.Chemicals.Add(addedChemical);
        }

        public void UnExecute()
        {
            table.Chemicals.Remove(addedChemical);
        }
    }
}
