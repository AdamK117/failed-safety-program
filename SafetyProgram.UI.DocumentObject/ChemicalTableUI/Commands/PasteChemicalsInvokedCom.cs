using System;
using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    internal sealed class PasteChemicalsInvokedCom : IInvokedCommand
    {
        private readonly ICollection<ICoshhChemical> targetTable;
        private readonly ICollection<ICoshhChemical> pastedChemicals;

        public PasteChemicalsInvokedCom(ICollection<ICoshhChemical> targetTable)
        {
            if (targetTable != null)
            {
                this.targetTable = targetTable;
            }
            else throw new ArgumentNullException();

            pastedChemicals = new List<ICoshhChemical>();
        }

        public void Execute()
        {
            pastedChemicals.Clear();
            //pastedChemicals.TryPaste();

            foreach (ICoshhChemical chemical in pastedChemicals)
            {
                targetTable.Add(chemical);
            }
        }

        public void UnExecute()
        {
            foreach (ICoshhChemical chemical in pastedChemicals)
            {
                targetTable.Remove(chemical);
            }
        }
    }
}
