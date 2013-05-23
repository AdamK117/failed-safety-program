using System.ComponentModel;
using MockApp.Commands;

namespace MockApp
{
    public interface IMainRibbonViewModel : INotifyPropertyChanged
    {
        IMainWindowCommands Commands { get; }
    }
}
