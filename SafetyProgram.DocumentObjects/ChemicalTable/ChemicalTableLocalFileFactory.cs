using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    public class ChemicalTableLocalFileFactory
        : ILocalFileFactory<ChemicalTable>
    {
        private readonly IConfiguration appConfiguration;

        public ChemicalTableLocalFileFactory(IConfiguration appConfiguration)
        {
            this.appConfiguration = appConfiguration;
        }

        public static ChemicalTable StaticCreateNew(IConfiguration appConfiguration)
        {
            return new ChemicalTable(
                appConfiguration, 
                new ObservableCollection<ICoshhChemicalObject>(), 
                "Chemical Table",
                (chemTable) => new ChemicalTableRibbonTab(chemTable),
                (chemTable) => new ChemicalTableView(chemTable)
            );
        }

        public ChemicalTable CreateNew()
        {
            return StaticCreateNew(appConfiguration);
        }

        public static ChemicalTable StaticLoad(XElement data, IConfiguration appConfiguration)
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

            return new ChemicalTable(appConfiguration, 
                loadedChemicals, 
                loadedHeader,
                (chemTable) => new ChemicalTableRibbonTab(chemTable),
                (chemTable) => new ChemicalTableView(chemTable)
                );
        }

        public ChemicalTable Load(XElement data)
        {
            return StaticLoad(data, appConfiguration);
        }

        public static XElement StaticStore(ChemicalTable item, IConfiguration appConfiguration)
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
            return StaticStore(item, appConfiguration);
        }

        public const string XML_IDENTIFIER = "chemicaltable";

        public string XmlIdentifier
        {
            get { return XML_IDENTIFIER; }
        }
    }
}
