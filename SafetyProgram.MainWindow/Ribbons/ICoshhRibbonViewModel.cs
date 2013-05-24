using System.Collections.ObjectModel;
using System.ComponentModel;
using Fluent;
using SafetyProgram.MainWindow.Commands;

namespace SafetyProgram.MainWindow.Ribbons
{
    public interface ICoshhRibbonViewModel : INotifyPropertyChanged
    {
        ObservableCollection<RibbonTabItem> RibbonTabs { get; }
        IWindowCommands Commands { get; }
    }
}
