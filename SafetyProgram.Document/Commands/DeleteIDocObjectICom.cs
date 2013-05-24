using System;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document.Commands
{
    public sealed class DeleteIDocObjectICom : ICommand
    {
        private readonly IDocumentBody body;
        private readonly ICommandInvoker commandInvoker;

        /// <summary>
        /// Construct an ICommand that deletes the currently selected item in the CoshhDocument.
        /// </summary>
        /// <param name="document">CoshhDocument from which selected items will be deleted.</param>
        public DeleteIDocObjectICom(IDocumentBody document, ICommandInvoker commandInvoker)
        {
            if (document != null && commandInvoker != null)
            {
                this.commandInvoker = commandInvoker;
                this.body = document;
                //Monitor changes in the CoshhDocument's selection (can't delete if there isn't a selection).
                document.SelectionChanged += (object sender, GenericPropertyChangedEventArg<IDocumentObject> docObject) => CanExecuteChanged.Raise(this);
            }
            else throw new ArgumentNullException();
        }    

        /// <summary>
        /// Can only execute if there is currently something selected
        /// </summary>
        /// <param name="parameter">Unused paramater</param>
        /// <returns></returns>
        public bool CanExecute(object parameter)
        {
            return (body.Selection == null) ? false : true;
        }


        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                var invokedCommand = new DeleteIDocObjectInvokedCom(
                    body,
                    commandInvoker
                    );
                commandInvoker.InvokeCommand(invokedCommand);
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)"); 
        }

        public event EventHandler CanExecuteChanged;
    }
}
