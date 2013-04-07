namespace SafetyProgram.MainWindow.Commands
{
    public abstract class WindowICommandBase : BaseICommand
    {
        protected CoshhWindow window;

        public WindowICommandBase(CoshhWindow window)
        {
            this.window = window;
        }
    }
}
