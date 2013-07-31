using SafetyProgram.Core.Commands;

namespace SafetyProgram.Document.Ribbons
{
    public interface IDocumentRibbonTabViewModel
    {
        IDocumentICommands Commands { get; }
    }
}
