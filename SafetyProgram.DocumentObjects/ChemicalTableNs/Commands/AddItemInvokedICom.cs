using System.Collections.Generic;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Commands
{
    internal sealed class AddItemInvokedICom<T> : IInvokedCommand
    {
        private readonly ICollection<T> items;
        private readonly T addedItem;

        public AddItemInvokedICom(ICollection<T> items, T addedItem)
        {
            if (Helpers.NullCheck(items, addedItem))
            {
                this.items = items;
                this.addedItem = addedItem;
            }
        }

        public void Execute()
        {
            items.Add(addedItem);
        }

        public void UnExecute()
        {
            items.Remove(addedItem);
        }
    }
}
