using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.UI.MainWindow
{
    internal sealed class MainViewModel : 
        IMainViewModel
    {
        public MainViewModel(Ribbon ribbonView,
            Control contentView)
        {
            Helpers.NullCheck(ribbonView,
                contentView);

            this.RibbonView = ribbonView;
            this.ContentView = contentView;
        }

        public Ribbon RibbonView { get; private set; }

        public Control ContentView { get; private set; }
    }
}
