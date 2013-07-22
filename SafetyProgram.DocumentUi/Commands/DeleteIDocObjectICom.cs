using System;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.GenericCommands;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentUi.Commands
{
    public sealed class DeleteIDocObjectICom : ICommand
    {
        private readonly IDocumentBody body;
        private readonly ICommandInvoker commandInvoker;

        public DeleteIDocObjectICom(IDocumentBody document, ICommandInvoker commandInvoker)
        {
            Helpers.NullCheck(document, commandInvoker);

            this.commandInvoker = commandInvoker;
            this.body = document;

            document.SelectionChanged += (object sender, GenericPropertyChangedEventArg<IDocumentObject> docObject) => CanExecuteChanged.Raise(this);
        }    

        public bool CanExecute(object parameter)
        {
            return (body.Selection == null) ? false : true;
        }

        public void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                var invokedCommand = new DeleteSelectedInvokedCom<IDocumentObject>(
                    body.Selection,
                    body.Items);
                commandInvoker.InvokeCommand(invokedCommand);
            }
            else throw new NotSupportedException("Call to execute made when it cant execute (CanExecute() == false)"); 
        }

        public event EventHandler CanExecuteChanged;
    }
}
