using System.Xml.Linq;

namespace SafetyProgram.Base.Interfaces
{
    public interface ILocalFileFactory<T> : IFactoryIO<T, XElement>
    {
        string XmlIdentifier { get; }
    }
}
