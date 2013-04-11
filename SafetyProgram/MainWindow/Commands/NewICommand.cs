using SafetyProgram.Document;

namespace SafetyProgram.MainWindow.Commands
{
    public class NewICommand : WindowICommandBase
    {
        public NewICommand(CoshhWindow window)
            : base(window)
        { }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            window.Service.New();
        }
    }
}