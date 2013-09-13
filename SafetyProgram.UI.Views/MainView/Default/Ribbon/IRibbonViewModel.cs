using System.Collections.ObjectModel;
using Fluent;
using SafetyProgram.Core.Commands.ICommands;

namespace SafetyProgram.UI.Views.MainView.Default
{
    /// <summary>
    /// Defines a viewmodel for the ribbon view.
    /// </summary>
    public interface IRibbonViewModel
    {
        /// <summary>
        /// Get the ribbon tabs for this ribbon.
        /// </summary>
        ObservableCollection<RibbonTabItem> RibbonTabs { get; }

        /// <summary>
        /// Get the commands available to the ribbon.
        /// </summary>
        /// <example>New, Load, Close, SaveAs, etc.</example>
        ICoreCommands Commands { get; }
    }
}
