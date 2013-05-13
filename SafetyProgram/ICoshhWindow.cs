using System.ComponentModel;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Commands;

namespace SafetyProgram
{
    public interface ICoshhWindow : 
        IContentWindow,
        IRibbonWindow,
        INotifyPropertyChanged
    {
        IWindowCommands Commands { get; }
    }
}
