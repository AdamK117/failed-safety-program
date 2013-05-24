using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Fluent;

namespace SafetyProgram.MainWindow
{
    public interface ICoshhWindowViewModel : INotifyPropertyChanged
    {
        Ribbon RibbonView { get; }
        Control ContentView { get; }
        List<InputBinding> Hotkeys { get; }
    }
}