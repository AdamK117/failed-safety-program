using System.Xml.Linq;

namespace SafetyProgram.Base.Interfaces
{
    interface IConfigurableStorable<T> : IStorable<T>
    {
        new T LoadFromXml(IConfiguration appConfig, XElement data);
    }
}
