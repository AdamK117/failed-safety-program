using System.Collections.Generic;

namespace SafetyProgram.Base.GenericCommands
{
    /// <summary>
    /// Defines an invoked command that adds an item to a collection.
    /// </summary>
    /// <typeparam name="T">The type of item handled by this command.</typeparam>
    public sealed class AddItemInvokedICom<T> : IInvokedCommand
    {
        private readonly ICollection<T> items;
        private readonly T addedItem;

        public AddItemInvokedICom(ICollection<T> items, T addedItem)
        {
            Helpers.NullCheck(items, addedItem);

            this.items = items;
            this.addedItem = addedItem;
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
