using System;
using System.Collections.Generic;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class DeleteSelectedInvokedCom<T> : IInvokedCommand
    {
        private readonly IEnumerable<T> selection;
        private readonly ICollection<T> items;

        private IEnumerable<T> deletedItems;

        public DeleteSelectedInvokedCom(IEnumerable<T> selectedChemicals,
            ICollection<T> chemicals)
        {
            if (selectedChemicals == null ||
                chemicals == null)
                throw new ArgumentNullException();
            else
            {
                this.selection = selectedChemicals;
                this.items = chemicals;
            }      
        }

        public void Execute()
        {
            deletedItems = new List<T>(selection);

            foreach (T item in deletedItems)
            {
                items.Remove(item);
            }
        }

        public void UnExecute()
        {
            foreach (T item in deletedItems)
            {
                items.Add(item);
            }
        }
    }
}
