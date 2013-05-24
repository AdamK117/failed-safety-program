using System;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document.Commands
{
    internal sealed class DeleteIDocObjectInvokedCom : IInvokedCommand
    {
        private readonly IDocumentBody body;
        private readonly ICommandInvoker commandInvoker;

        private IDocumentObject deletedItem;
        private int deletedItemIndex;

        public DeleteIDocObjectInvokedCom(
            IDocumentBody document,
            ICommandInvoker commandInvoker
            )
        {
            if (document != null && commandInvoker != null)
            {
                this.body = document;
                this.commandInvoker = commandInvoker;
            }
            else throw new ArgumentNullException();
        }

        public void Execute()
        {
            deletedItem = body.Selection;
            deletedItemIndex = body.Items.IndexOf(deletedItem);

            body.Items.Remove(deletedItem);
            body.DeSelectAll();
        }

        public void UnExecute()
        {
            body.Items.Insert(deletedItemIndex, deletedItem);
        }
    }
}
