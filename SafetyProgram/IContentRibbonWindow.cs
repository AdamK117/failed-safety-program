using System.ComponentModel;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Commands;

namespace SafetyProgram
{
    public interface IContentRibbonWindow : 
        IContentWindow, 
        IRibbonWindow, 
        INotifyPropertyChanged
    {
        IWindowCommands Commands { get; }
    }
}
