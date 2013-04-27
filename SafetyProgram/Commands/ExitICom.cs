using SafetyProgram.Base.Interfaces;
using SafetyProgram.Base;
namespace SafetyProgram.Commands
{
    public sealed class ExitICom : ExtendedICommand<IWindow<IDocument>>
    {
        public ExitICom(IWindow<IDocument> window)
            : base(window)
        { }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override void Execute(object parameter)
        {
            data.View.Close();
        }
    }
}
