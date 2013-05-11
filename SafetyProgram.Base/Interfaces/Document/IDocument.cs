using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SafetyProgram.Base.Interfaces
{
    public interface IDocument : IViewable, IInteractable, IDataErrorInfo
    {
        ObservableCollection<IRibbonTabItem> RibbonTabs { get; }

        string Title { get; set; }
        event Action<string> TitleChanged;

        IDocumentBody Body { get; }

        IDocFormat Format { get; }
        void ChangeFormat(IDocFormat newFormat);
        event Action<IDocFormat> FormatChanged;
    }
}
