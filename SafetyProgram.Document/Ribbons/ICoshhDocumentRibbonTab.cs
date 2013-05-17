using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Commands;

namespace SafetyProgram.Document.Ribbons
{
    internal interface ICoshhDocumentRibbonTab : IRibbonTabItem
    {
        IDocumentICommands DocumentCommands { get; }
    }
}
