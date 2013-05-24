using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document
{
    public interface ICoshhDocumentViewModel : INotifyPropertyChanged
    {
        IFormat Format { get; }
        ContextMenu ContextMenu { get; }
        IDocumentBody Body { get; }
        List<InputBinding> Hotkeys { get; }
    }
}
