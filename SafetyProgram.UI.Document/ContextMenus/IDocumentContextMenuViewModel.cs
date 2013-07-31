using SafetyProgram.Core.Commands;

namespace SafetyProgram.UI.Document
{
    public interface IDocumentContextMenuViewModel
    {
        IDocumentICommands Commands { get; }
    }
}
