namespace SafetyProgram.MainWindow.Commands
{
    public class ExitICommand : WindowICommandBase
    {
        public ExitICommand(CoshhWindow window)
            : base(window)
        { }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            window.View.Close();
        }
    }
}
