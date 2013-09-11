using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands;

namespace SafetyProgram.UI
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
        ReadOnlyObservableCollection<RibbonTabItem> RibbonTabs { get; }

        /// <summary>
        /// Get the commands available to the ribbon.
        /// </summary>
        /// <example>New, Load, Close, SaveAs, etc.</example>
        ICoreCommands Commands { get; }
    }
}
