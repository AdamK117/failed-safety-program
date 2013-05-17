using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Commands;

namespace SafetyProgram.Document.ContextMenus
{
    interface IDocumentContextMenu : 
        IContextMenu
    {
        IDocumentICommands DocumentCommands { get; }
    }
}
