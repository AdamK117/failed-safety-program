using System.ComponentModel;
using System.Windows.Controls;
using Fluent;
using MockApp.Commands;

namespace MockApp
{
    public interface IMainWindowViewModel : INotifyPropertyChanged
    {
        Ribbon Ribbon { get; }
        Control Content { get; }
        IMainWindowCommands Commands { get; }
    }
}
