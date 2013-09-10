using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Core.IO;

namespace SafetyProgram.Core.Models.Serialization
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
            string loadedHeader;
            ObservableCollection<ICoshhChemical> loadedChemicals;

            // Header (optional)
            {
                var headerElement = data.Element("header");

                loadedHeader =
                    headerElement != null ?
                        headerElement.Value :
                        "Default Header";
            }

            // Chemicals (optional, table may be empty).
            {
                var chemicalSerializer = new CoshhChemicalXml();

                loadedChemicals = new ObservableCollection<ICoshhChemical>(
                    from chemicalElement in data.Elements("coshhchemical")
                    select chemicalSerializer.Load(chemicalElement));
            }

            return new ChemicalTable(loadedHeader, loadedChemicals);
        }
    }
}
