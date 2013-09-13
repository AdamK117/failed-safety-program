using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.UI.MainView.Default
{
    /// <summary>
    /// Defines a standard implementation of an <code>IMainViewModel</code>
    /// </summary>
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

        /// <summary>
        /// Get the ribbon view.
        /// </summary>
        public Ribbon RibbonView { get; private set; }

        /// <summary>
        /// Get the content view.
        /// </summary>
        public Control ContentView { get; private set; }
    }
}
