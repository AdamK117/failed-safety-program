using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document
{
    public interface ICoshhDocument : IWindowContent
    {
        IDocumentBody Body { get; }
        IFormat Format { get; }
    }
}
