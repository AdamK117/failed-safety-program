using System.Xml.Linq;

namespace SafetyProgram.Base.Interfaces
{
    public interface ILocalFileFactory<T> : IFactory<T, XElement>
    {
        string XmlIdentifier { get; }
    }
}
