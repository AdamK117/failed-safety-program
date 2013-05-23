using System.Windows;
using System.Windows.Controls;
using SafetyProgram.Base.Interfaces;

namespace MockApp.MainWindow
{
    public interface IMainWindow<TContent>
    {
        Window View { get; }
        IHolderT<TContent> Content { get; }
        IHolderT<IIOService<TContent>> Service { get; }
    }
}
