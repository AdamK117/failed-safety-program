using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using SafetyProgram.BaseClasses;
using SafetyProgram.DocObjects;
using SafetyProgram.Document.Commands;

namespace SafetyProgram.Document
{
    public interface ICoshhDocument
    {
        UserControl View { get; }
        ObservableCollection<IDocObject> Body { get; }
        DocumentCommandsHolder Commands { get; }

        string Title { get; set; }
        event Action<string> TitleChanged;       

        IDocFormat Format { get; }
        void ChangeFormat(IDocFormat newFormat);
        event Action<IDocFormat> FormatChanged;        

        void Select(IDocObject docObject);
        void ClearSelection();
        IDocObject Selected { get; }
        event Action<IDocObject> SelectionChanged;

        void Edited();
        bool EditedFlag { get; }
        event Action<bool> EditedFlagChanged;
    }
}
