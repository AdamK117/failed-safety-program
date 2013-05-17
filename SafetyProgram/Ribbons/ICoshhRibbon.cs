using System.ComponentModel;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Commands;

namespace SafetyProgram.Ribbons
{
    internal interface ICoshhRibbon : 
        INotifyPropertyChanged, 
        IRibbon
    {
        IWindowCommands WindowCommands { get; }
        bool RibbonVisibility { get; }
    }
}
