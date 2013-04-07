namespace SafetyProgram.MainWindow.Commands
{
    public class OpenICommand : WindowICommandBase
    {
        public OpenICommand(CoshhWindow window)
            : base(window)
        { }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            window.Service.Load();
        }
    }
}
