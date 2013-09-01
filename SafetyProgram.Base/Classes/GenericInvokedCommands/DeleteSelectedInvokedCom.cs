using System.Collections.Generic;
using System.Collections.ObjectModel;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Base.GenericCommands
{
    public sealed class DeleteSelectedInvokedCom<T> : IInvokedCommand
    {
        private readonly IEnumerable<T> selection;
        private readonly IList<T> items;

        private IList<T> deletedItems = new List<T>();
        private IList<int> deletedItemsIndexes = new List<int>();

        //For multiple selection.
        public DeleteSelectedInvokedCom(IEnumerable<T> selection,
            IList<T> items)
        {
            Helpers.NullCheck(selection, items);

            this.selection = selection;
            this.items = items;
        }

        //For single selection.
        public DeleteSelectedInvokedCom(T selection,
            IList<T> items)
        {
            Helpers.NullCheck(selection, items);

            this.selection = new List<T>(){ selection };
            this.items = items;
        }

        public void Execute()
        {
            //Placeholder list, prevents foreach exception when modifying list.
            ReadOnlyCollection<T> unmodifiedSelectedItems = new List<T>(selection).AsReadOnly();

            foreach (T selectedItem in unmodifiedSelectedItems)
            {
                //Store the item to be deleted.
                deletedItems.Add(selectedItem);

                //Store the index of the item to be deleted.
                deletedItemsIndexes.Add(
                    items.IndexOf(selectedItem)
                );

                //Delete it.
                items.Remove(selectedItem);
            }
        }

        public void UnExecute()
        {
            //Go in reverse, un-deleting the items in the correct order.
            //Indexing requires that the operation is done in reverse.
            for (int i = deletedItems.Count - 1; 
                i >= 0; 
                i--)
            {
                items.Insert(deletedItemsIndexes[i], deletedItems[i]);
            }
        }
    }
}
