using System;
using System.Xml.Linq;
using SafetyProgram.Core.IO;

namespace SafetyProgram.Core.Models.Serialization
{
    public sealed class DocumentObjectXml :
        IStorageConverter<IDocumentObject, XElement>
    {
        public XElement Store(IDocumentObject data)
        {
            throw new NotImplementedException();
        }

        public IDocumentObject Load(XElement data)
        {
            switch (data.Name.LocalName)
            {
                case "chemicaltable":
                    var chemicalSerializer = new ChemicalTableXml();
                    return chemicalSerializer.Load(data);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
