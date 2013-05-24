using System;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document.Commands
{
    internal sealed class InsertIDocumentObjectInvokedCom : IInvokedCommand
    {
        private readonly IDocumentBody body;
        private readonly Func<IDocumentObject> iDocumentObjectCtor;
        private IDocumentObject addedDocumentObject;

        public InsertIDocumentObjectInvokedCom(
            IDocumentBody document,
            Func<IDocumentObject> documentObjectConstructor
            )
        {
            if (document != null && documentObjectConstructor != null)
            {
                this.body = document;
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
            body.Items.Add(addedDocumentObject);
        }

        public void UnExecute()
        {
            body.Items.Remove(addedDocumentObject);
        }
    }
}
