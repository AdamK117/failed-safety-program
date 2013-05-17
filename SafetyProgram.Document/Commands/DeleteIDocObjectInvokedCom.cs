using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document.Commands
{
    internal sealed class DeleteIDocObjectInvokedCom : IInvokedCommand
    {
        private readonly IDocument document;
        private readonly ICommandInvoker commandInvoker;
        private IDocumentObject deletedItem;
        private int deletedItemIndex;

        public DeleteIDocObjectInvokedCom(
            IDocument document,
            ICommandInvoker commandInvoker
            )
        {
            if (document != null && commandInvoker != null)
            {
                this.document = document;
                this.commandInvoker = commandInvoker;
            }
            else throw new ArgumentNullException();
        }

        public void Execute()
        {
            var documentBody = document.Body;

            deletedItem = documentBody.Selection;
            deletedItemIndex = documentBody.Items.IndexOf(deletedItem);

            documentBody.Items.Remove(deletedItem);
            documentBody.DeSelectAll();
        }

        public void UnExecute()
        {
            var documentBody = document.Body;

            documentBody.Items.Insert(deletedItemIndex, deletedItem);
        }
    }
}
