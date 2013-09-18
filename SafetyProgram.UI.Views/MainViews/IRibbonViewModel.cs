using System.Collections.Generic;
using System.ComponentModel;
using Fluent;

namespace SafetyProgram.UI.Views.MainViews
{
    /// <summary>
    /// Defines a viewmodel for the ribbon view.
    /// </summary>
    public interface IRibbonViewModel : 
        INotifyPropertyChanged
    {
        /// <summary>
        /// Get the ribbon tabs for this ribbon.
        /// </summary>
        IEnumerable<RibbonTabItem> RibbonTabs { get; }
    }
}
