using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using SafetyProgram.Base.Interfaces;

namespace MockApp.Document
{
    public interface IDocumentViewModel
    {
        ContextMenu ContextMenu { get; }
        ObservableCollection<Control> Content { get; }
        IFormat Format { get; }
    }
}
