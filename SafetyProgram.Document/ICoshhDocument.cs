using System.ComponentModel;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Commands;

namespace SafetyProgram.Document
{
    public interface ICoshhDocument :
        INotifyPropertyChanged,
        IDocument
    {
        IConfiguration AppConfiguration { get; }
        IDocumentICommands Commands { get; }
    }
}
