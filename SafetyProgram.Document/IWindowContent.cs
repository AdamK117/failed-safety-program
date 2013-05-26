using System.Collections.Generic;
using System.Collections.ObjectModel;
using Fluent;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document
{
    public interface IWindowContent : IViewable
    {
        ICollection<RibbonTabItem> DocumentRibbonTabs { get; }
        ObservableCollection<RibbonTabItem> ContextualRibbonTabs { get; }
    }
}
