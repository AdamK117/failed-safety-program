using System.Xml.Linq;

namespace SafetyProgram.Base.Interfaces
{
    public interface ILocalFileFactory<T> : IIOFactory<T, XElement>
    {
        string XmlIdentifier { get; }
    }
}
