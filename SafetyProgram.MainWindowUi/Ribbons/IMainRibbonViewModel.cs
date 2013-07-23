using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.MainWindowUi.Commands;

namespace SafetyProgram.MainWindowUi.Ribbons
{
    /// <summary>
    /// Defines an interface for the viewmodel of main windows ribbon.
    /// </summary>
    public interface IMainRibbonViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Get the documents ribbon tabs.
        /// </summary>
        /// <example>Insert, Format, Page Setup</example>
        ICollection<RibbonTabItem> DocumentRibbonTabs { get; }

        /// <summary>
        /// Occurs when the DocumentRibbonTabs changes.
        /// </summary>
        event EventHandler<GenericPropertyChangedEventArg<ICollection<RibbonTabItem>>> DocumentRibbonTabsHolderChanged;

        /// <summary>
        /// Get any contextual ribbon tabs that the document may be broadcasting.
        /// </summary>
        /// <example>ChemicalTable contextual tab, Chemical contextual tab.</example>
        ObservableCollection<RibbonTabItem> ContextualRibbonTabs { get; }

        /// <summary>
        /// Occurs when the contextual ribbon tabs change.
        /// </summary>
        event EventHandler<GenericPropertyChangedEventArg<ObservableCollection<RibbonTabItem>>> ContextualRibbonTabsHolderChanged;

        /// <summary>
        /// Get the commands available to the ribbon tab.
        /// </summary>
        /// <example>New, Load, Close, SaveAs, etc.</example>
        IMainWindowCommands Commands { get; }
    }
}
