using System;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document.Commands
{
    internal sealed class InsertIDocumentObjectInvokedCom : IInvokedCommand
    {
        private readonly IDocument data;
        private readonly Func<IDocumentObject> iDocumentObjectCtor;
        private IDocumentObject addedDocumentObject;

        public InsertIDocumentObjectInvokedCom(
            IDocument document,
            Func<IDocumentObject> documentObjectConstructor
            )
        {
            if (document != null && documentObjectConstructor != null)
            {
                this.data = document;
                this.iDocumentObjectCtor = documentObjectConstructor;
            }
            else throw new ArgumentNullException();            
        }

        public void Execute()
        {
            if (addedDocumentObject == null)
            {
                addedDocumentObject = iDocumentObjectCtor();
            }            
            data.Body.Items.Add(addedDocumentObject);
        }

        public void UnExecute()
        {
            data.Body.Items.Remove(addedDocumentObject);
        }
    }
}
