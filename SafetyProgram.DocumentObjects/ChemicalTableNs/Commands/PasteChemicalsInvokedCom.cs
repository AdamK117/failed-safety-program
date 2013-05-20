using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class PasteChemicalsInvokedCom : IInvokedCommand
    {
        private readonly IChemicalTable table;
        private readonly ICollection<ICoshhChemicalObject> pastedChemicals;

        public PasteChemicalsInvokedCom(IChemicalTable table)
        {
            if (table != null)
            {
                this.table = table;
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
                table.Chemicals.Add(chemical);
            }
        }

        public void UnExecute()
        {
            foreach (ICoshhChemicalObject chemical in pastedChemicals)
            {
                table.Chemicals.Remove(chemical);
            }
        }
    }
}
