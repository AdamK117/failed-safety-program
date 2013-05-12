using SafetyProgram.Base.Interfaces;
using SafetyProgram.Commands;

namespace SafetyProgram
{
    public interface ICoshhWindow 
        : IWindow<IDocument>, IRibbonWindow
    {
        IWindowCommands Commands { get; }
    }
}
