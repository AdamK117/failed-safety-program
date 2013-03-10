using System.ComponentModel;

namespace SafetyProgram.ICommands
{
    public class SaveAsPDFICommand : DOMBase
    {

        public SaveAsPDFICommand() : base()
        {
            coshhWindow.Document.IsOpenChanged += new Data.DOM.CoshhDocument.isOpenChangedDelegate(Document_IsOpenChanged);
        }

        void Document_IsOpenChanged(bool isOpen)
        {
            canExecute = isOpen;
            RaiseCanExecuteChanged();
        }

        public override void Execute(object parameter)
        {
            if (CanExecute(parameter))
            {
                coshhWindow.SaveAsPDF();
            }
        }
    }
}
