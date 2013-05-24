using System.Collections.ObjectModel;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document
{
    public interface ICoshhDocument
    {
        Control View { get; }
        IDocumentBody Body { get; }
        IFormat Format { get; }
        ObservableCollection<RibbonTabItem> RibbonTabs { get; }
    }
}
