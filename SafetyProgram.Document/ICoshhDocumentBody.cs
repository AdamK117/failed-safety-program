using System;
using System.Collections.ObjectModel;
using SafetyProgram.DocObjects;

namespace SafetyProgram.Document
{
    public interface ICoshhDocumentBody
    {
        ObservableCollection<IDocObject> Items { get; }

        IDocObject Selection { get; }
        void Select(IDocObject item);
        void DeSelect(IDocObject item);
        void DeSelectAll();        
        event Action<IDocObject> SelectionChanged;
    }
}
