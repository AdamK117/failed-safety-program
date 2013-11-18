using Fluent;
using System.Collections.ObjectModel;

namespace SafetyProgram.UI.Views.MainViews
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
    }
}
