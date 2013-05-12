using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    /// <summary>
    /// Defines an ABSTRACT FACTORY for producing IDocumentObjects
    ///     -Works with local files (XML)
    ///     -Produces a ChemicalTable IDocumentObject
    /// </summary>
    internal sealed class ChemicalTableLocalFileFactory
        : ILocalFileFactory<ChemicalTable>
    {
        private readonly IConfiguration appConfiguration;

        public ChemicalTableLocalFileFactory(IConfiguration appConfiguration)
        {
            this.appConfiguration = appConfiguration;
        }

        public static ChemicalTable StaticCreateNew(IConfiguration appConfiguration)
        {
            return ChemicalTableDefaults.DefaultTable(appConfiguration);
        }

        public ChemicalTable CreateNew()
        {
            return StaticCreateNew(appConfiguration);
        }

        public static ChemicalTable StaticLoad(XElement data, IConfiguration appConfiguration)
        {
            //Define variables to load from the data
            string loadedHeader = "";
            var loadedChemicals = new ObservableCollection<ICoshhChemicalObject>();

            //Header (Optional)
            {
                var headerElement = data.Element("header");
                if (headerElement != null)
                {
                    loadedHeader = headerElement.Value;
                }
                else loadedHeader = "SomeDefaultValue";
            }

            //Chemicals in the table (Optional)
            {
                var coshhChemicalsElements = data.Elements(CoshhChemicalObjectLocalFileFactory.XML_IDENTIFIER);
                foreach (XElement coshhChemicalElement in coshhChemicalsElements)
                {
                    var chemicalObject = CoshhChemicalObjectLocalFileFactory.StaticLoad(coshhChemicalElement);
                    loadedChemicals.Add(chemicalObject);
                }
            }

            //Return the fully loaded chemical table
            return new ChemicalTable(
                appConfiguration,
                loadedChemicals,
                loadedHeader,
                ChemicalTableDefaults.DefaultCommandsConstructor,
                ChemicalTableDefaults.DefaultContextMenuConstructor,
                ChemicalTableDefaults.DefaultRibbonConstructor,
                ChemicalTableDefaults.DefaultViewConstructor
                );
        }

        public ChemicalTable Load(XElement data)
        {
            return StaticLoad(data, appConfiguration);
        }

        public static XElement StaticStore(ChemicalTable item, IConfiguration appConfiguration)
        {
            //TODO: Validation check
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
