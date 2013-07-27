using System;
using System.Xml.Linq;
using SafetyProgram.IO;

namespace SafetyProgram.Models.IO.XML
{
    /// <summary>
    /// Defines an implementation of an IChemicalTable<-->XML (de)serializer.
    /// </summary>
    public sealed class ChemicalTableXml : IStorageConverter<IChemicalTable, XElement>
    {
        /// <summary>
        /// Construct an instance of an IChemcialTable<-->XML (de)serializer.
        /// </summary>
        public ChemicalTableXml()
        { }

        /// <summary>
        /// Serialize the chemical table data in memory to an XML format.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public XElement Store(IChemicalTable data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize chemical table data into an object format.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public IChemicalTable Load(XElement data)
        {
            throw new NotImplementedException();
        }
    }
}
