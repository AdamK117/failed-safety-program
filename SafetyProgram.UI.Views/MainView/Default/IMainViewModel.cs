using System.ComponentModel;
using System.Windows.Controls;
using Fluent;

namespace SafetyProgram.UI.Views.MainView.Default
{
    /// <summary>
    /// Defines an interface for the ViewModel of the CoshhWindowView.
    /// </summary>
    public interface IMainViewModel :
        INotifyPropertyChanged
    {
        /// <summary>
        /// Get the RibbonView associated with the ViewModel.
        /// </summary>
        Ribbon RibbonView { get; }

        /// <summary>
        /// Get the Content view associated with the ViewModel.
        /// </summary>
        Control ContentView { get; }
    }
}