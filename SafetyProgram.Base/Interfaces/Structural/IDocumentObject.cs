using Fluent;

namespace SafetyProgram.Base.Interfaces
{
    public interface IDocumentObject : 
        IViewable,
        IEditable
    {
        RibbonTabItem ContextualTab { get; }
    }
}
