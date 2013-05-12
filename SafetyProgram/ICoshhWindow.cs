using SafetyProgram.Base.Interfaces;
using SafetyProgram.Commands;
using SafetyProgram.Document;

namespace SafetyProgram
{
    public interface ICoshhWindow : 
        IWindow<CoshhDocument>, 
        IContentRibbonWindow
    {
        IWindowCommands Commands { get; }
    }
}
