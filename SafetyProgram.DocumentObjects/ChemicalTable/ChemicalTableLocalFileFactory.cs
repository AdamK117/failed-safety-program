using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.ModelObjects;
using SafetyProgram.Static;

namespace SafetyProgram.DocumentObjects.ChemicalTable
{
    public class ChemicalTableLocalFileFactory
        : ILocalFileFactory<ChemicalTable>
    {
        public static ChemicalTable StaticCreateNew()
        {
            return new ChemicalTable();
        }

        public ChemicalTable CreateNew()
        {
            return StaticCreateNew();
        }

        public static ChemicalTable StaticLoad(XElement data)
        {
            string loadedHeader = "";
            var loadedChemicals = new ObservableCollection<ICoshhChemicalObject>();

            var headerElement = data.Element("header");
            if (headerElement != null)
            {
                loadedHeader = headerElement.Value;
            }

            var coshhChemicalsElements = data.Elements(CoshhChemicalObjectLocalFileFactory.XML_IDENTIFIER);
            foreach (XElement coshhChemicalElement in coshhChemicalsElements)
            {
                var chemicalObject = CoshhChemicalObjectLocalFileFactory.StaticLoad(coshhChemicalElement);
                loadedChemicals.Add(chemicalObject);
            }

            return new ChemicalTable(loadedChemicals, loadedHeader);
        }

        public ChemicalTable Load(XElement data)
        {
            return StaticLoad(data);
        }

        public static XElement StaticStore(ChemicalTable item)
        {
            return (
                new XElement(XML_IDENTIFIER,
                    new XElement("header", item.Header),
                    item.Chemicals.Count > 0 ?
                        from chemical in item.Chemicals
                        select CoshhChemicalObjectLocalFileFactory.StaticStore(chemical)
                    :
                        null
                )
            );
        }

        public XElement Store(ChemicalTable item)
        {
            return StaticStore(item);
        }

        public const string XML_IDENTIFIER = "chemicaltable";

        public string XmlIdentifier
        {
            get { return XML_IDENTIFIER; }
        }
    }
}
