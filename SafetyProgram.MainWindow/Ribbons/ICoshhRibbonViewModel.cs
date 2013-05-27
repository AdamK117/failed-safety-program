using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.MainWindow.Commands;

namespace SafetyProgram.MainWindow.Ribbons
{
    public interface ICoshhRibbonViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the documents ribbon tabs.
        /// </summary>
        /// <example>Insert, Format, Page Setup</example>
        ICollection<RibbonTabItem> DocumentRibbonTabs { get; }
        event EventHandler<GenericPropertyChangedEventArg<ICollection<RibbonTabItem>>> DocumentRibbonTabsHolderChanged;

        /// <summary>
        /// Gets any contextual ribbon tabs that the document may be broadcasting.
        /// </summary>
        /// <example>ChemicalTable contextual tab, Chemical contextual tab.</example>
        ObservableCollection<RibbonTabItem> ContextualRibbonTabs { get; }
        event EventHandler<GenericPropertyChangedEventArg<ObservableCollection<RibbonTabItem>>> ContextualRibbonTabsHolderChanged;

        /// <summary>
        /// Gets the commands available to the ribbon tab.
        /// </summary>
        /// <example>New, Load, Close, SaveAs, etc.</example>
        IWindowCommands Commands { get; }
    }
}
