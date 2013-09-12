using System.Collections.ObjectModel;
using Fluent;
using SafetyProgram.Core.Commands;

namespace SafetyProgram.UI.MainWindow
{
    /// <summary>
    /// Defines a viewmodel for the ribbon view.
    /// </summary>
    internal interface IRibbonViewModel
    {
        /// <summary>
        /// Get the ribbon tabs for this ribbon.
        /// </summary>
        ReadOnlyObservableCollection<RibbonTabItem> RibbonTabs { get; }

        /// <summary>
        /// Get the commands available to the ribbon.
        /// </summary>
        /// <example>New, Load, Close, SaveAs, etc.</example>
        ICoreCommands Commands { get; }
    }
}
