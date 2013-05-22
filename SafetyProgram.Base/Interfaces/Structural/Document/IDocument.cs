using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace SafetyProgram.Base.Interfaces
{
    public interface IDocument : 
        IViewable, 
        IInteractable, 
        IDataErrorInfo
    {
        ObservableCollection<IRibbonTabItem> RibbonTabs { get; }

        string Title { get; set; }
        event EventHandler<GenericPropertyChangedEventArg<string>> TitleChanged;

        IDocumentBody Body { get; }

        IFormat Format { get; }
        void ChangeFormat(IFormat newFormat);
        event EventHandler<GenericPropertyChangedEventArg<IFormat>> FormatChanged;
    }
}
