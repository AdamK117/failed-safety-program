using System.Windows.Input;

namespace SafetyProgram.Base.Interfaces
{
    public interface IInvokedCommand
    {
        void Execute();
        void UnExecute();
    }
}
