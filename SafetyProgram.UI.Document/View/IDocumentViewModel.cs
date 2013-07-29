using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Models;

namespace SafetyProgram.UI.Document
{
    public interface IDocumentViewModel : INotifyPropertyChanged
    {
        IFormat Format { get; }
        ContextMenu ContextMenu { get; }
        List<InputBinding> Hotkeys { get; }
    }
}
