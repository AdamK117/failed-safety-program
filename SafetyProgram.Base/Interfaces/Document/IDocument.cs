using System;
using System.Collections.ObjectModel;

namespace SafetyProgram.Base.Interfaces
{
    public interface IDocument : IViewable, IStorable<IDocument>, IInteractable
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
