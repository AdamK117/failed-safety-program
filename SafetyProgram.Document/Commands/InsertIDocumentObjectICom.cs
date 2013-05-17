using System;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document.Commands
{
    public sealed class InsertIDocumentObjectICom : ICommand
    {
        private readonly IDocument document;
        private readonly ICommandInvoker commandInvoker;
        private readonly Func<IDocumentObject> iDocumentObjectCtor;
        private IDocumentObject addedDocumentObject;

        public InsertIDocumentObjectICom(
            IDocument document, 
            ICommandInvoker commandInvoker,
            Func<IDocumentObject> iDocumentObjectCtor
            )
        {
            if
                (
                document != null &&
                commandInvoker != null &&
                iDocumentObjectCtor != null
                )
            {
                this.document = document;
                this.commandInvoker = commandInvoker;
                this.iDocumentObjectCtor = iDocumentObjectCtor;
            }
            else throw new ArgumentNullException();
        }

        /// <summary>
        /// Can always execute.
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return true;
        }

        /// <summary>
        /// Inserts a ChemicalTable into the CoshhDocument
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <exception cref="NotSupportedException">Thrown if Execute is called but CanExecute == false</exception>
        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                var invokedCommand = new InsertIDocumentObjectInvokedCom(
                    document,
                    iDocumentObjectCtor
                    );
                commandInvoker.InvokeCommand(invokedCommand);
            }
        }

        public event EventHandler CanExecuteChanged;
    }
}
