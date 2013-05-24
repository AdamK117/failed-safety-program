using System;
using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class PasteChemicalsInvokedCom : IInvokedCommand
    {
        private readonly ICollection<ICoshhChemicalObject> targetTable;
        private readonly ICollection<ICoshhChemicalObject> pastedChemicals;

        public PasteChemicalsInvokedCom(ICollection<ICoshhChemicalObject> targetTable)
        {
            if (targetTable != null)
            {
                this.targetTable = targetTable;
            }
            else throw new ArgumentNullException();

            pastedChemicals = new List<ICoshhChemicalObject>();
        }

        public void Execute()
        {
            pastedChemicals.Clear();
            pastedChemicals.TryPaste();

            foreach (ICoshhChemicalObject chemical in pastedChemicals)
            {
                targetTable.Add(chemical);
            }
        }

        public void UnExecute()
        {
            foreach (ICoshhChemicalObject chemical in pastedChemicals)
            {
                targetTable.Remove(chemical);
            }
        }
    }
}
