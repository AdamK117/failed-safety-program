using System.ComponentModel;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.MainWindow.Commands;

namespace SafetyProgram.MainWindow
{
    public interface ICoshhWindow : 
        IContentWindow,
        IRibbonWindow,
        INotifyPropertyChanged
    {
        IWindowCommands Commands { get; }
    }
}
