using System.Windows;

namespace SafetyProgram.UI
{
    public interface IWindowController : 
        IUiController
    {
        new Window View { get; }
    }
}
