namespace SafetyProgram.Commands
{
    public class ExitICommand : CoshhWindowICommand
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
