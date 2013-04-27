using System;
using System.Collections.ObjectModel;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Base.Interfaces
{
    public interface IDocumentBody
    {
        ObservableCollection<IDocumentObject> Items { get; }

        IDocumentObject Selection { get; }
        void Select(IDocumentObject item);
        void DeSelect(IDocumentObject item);
        void DeSelectAll();        
        event Action<IDocumentObject> SelectionChanged;
    }
}
