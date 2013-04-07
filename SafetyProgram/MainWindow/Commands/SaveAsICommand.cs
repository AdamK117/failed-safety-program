namespace SafetyProgram.MainWindow.Commands
{
    public class SaveAsICommand : WindowICommandBase
    {
        public SaveAsICommand(CoshhWindow window)
            : base(window)
        {
            window.DocumentChanged += (document) => RaiseCanExecuteChanged();
        }

        public override bool CanExecute(object parameter)
        {
            return window.Document == null ? false : true;
        }

        public override void Execute(object parameter)
        {
            window.Service.SaveAs();
        }
    }
}