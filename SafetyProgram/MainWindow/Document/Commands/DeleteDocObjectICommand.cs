using System;
using SafetyProgram.MainWindow.Document.Controls;

namespace SafetyProgram.MainWindow.Document.Commands
{
    public class DeleteDocObjectICommand : DocumentCommandsBase
    {
        public DeleteDocObjectICommand(CoshhDocument document)
            : base(document)
        {
            document.SelectionChanged += (DocObject docObject) => RaiseCanExecuteChanged();
        }        

        public override bool CanExecute(object parameter)
        {
            return document.Selected == null ? false : true;
        }

        public override void Execute(object parameter)
        {
            document.Body.Remove(document.Selected);
            document.Selected = null;
        }
    }
}
