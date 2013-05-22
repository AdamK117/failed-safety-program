using System.ComponentModel;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.MainWindow.Commands;

namespace SafetyProgram.MainWindow.Ribbons
{
    internal interface ICoshhRibbon : 
        INotifyPropertyChanged, 
        IRibbon
    {
        IWindowCommands WindowCommands { get; }
        bool RibbonVisibility { get; }
    }
}
