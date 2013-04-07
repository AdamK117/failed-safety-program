using System.ComponentModel;
using System.Windows.Forms;

namespace SafetyProgram.MainWindow.Commands
{
    public class CloseICommand : WindowICommandBase
    {
        public CloseICommand(CoshhWindow window)
            : base(window)
        {
            //Add a handler that monitors if the document has been changed (new, closed, etc.)
            window.DocumentChanged += (document => RaiseCanExecuteChanged());
        }

        //May only close if there's actually a document.
        public override bool CanExecute(object parameter)
        {            
            return window.Document == null ? false : true;
        }

        public override void Execute(object parameter)
        {
            window.Service.Close();
        }
    }
}
