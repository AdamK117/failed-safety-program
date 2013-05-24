using SafetyProgram.Document.Commands;

namespace SafetyProgram.Document.Ribbons
{
    public interface ICoshhDocumentRibbonTabViewModel
    {
        IDocumentICommands Commands { get; }
    }
}
