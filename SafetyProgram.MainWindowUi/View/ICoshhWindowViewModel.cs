using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Fluent;

namespace SafetyProgram.MainWindowUi
{
    /// <summary>
    /// Defines an interface for the ViewModel of the CoshhWindowView.
    /// </summary>
    public interface ICoshhWindowViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Get the RibbonView associated with the ViewModel.
        /// </summary>
        Ribbon RibbonView { get; }

        /// <summary>
        /// Get the Content view associated with the ViewModel.
        /// </summary>
        Control ContentView { get; }

        /// <summary>
        /// Get the hotkeys associated with the ViewModel.
        /// </summary>
        List<InputBinding> Hotkeys { get; }
    }
}