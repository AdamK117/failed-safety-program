using System;
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
    public sealed class ChemicalTableLocalFileFactory
        : ILocalFileFactory<IChemicalTable>
    {
        private readonly IConfiguration appConfiguration;
        private readonly ICommandInvoker commandInvoker;

        public ChemicalTableLocalFileFactory(IConfiguration appConfiguration, ICommandInvoker commandInvoker)
        {
            if (appConfiguration != null && commandInvoker != null)
            {
                this.appConfiguration = appConfiguration;
                this.commandInvoker = commandInvoker;
            }
            else throw new ArgumentNullException();
        }

        public static IChemicalTable StaticCreateNew(IConfiguration appConfiguration, ICommandInvoker commandInvoker)
        {
            return ChemicalTableDefaults.DefaultTable(appConfiguration, commandInvoker);
        }

        public IChemicalTable CreateNew()
        {
            return StaticCreateNew(appConfiguration, commandInvoker);
        }

        public static IChemicalTable StaticLoad(XElement data, IConfiguration appConfiguration, ICommandInvoker commandInvoker)
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
                ChemicalTableDefaults.DefaultCommandsConstructor(commandInvoker),
                ChemicalTableDefaults.DefaultContextMenuConstructor,
                ChemicalTableDefaults.DefaultRibbonConstructor,
                ChemicalTableDefaults.DefaultViewConstructor
                );
        }

        public IChemicalTable Load(XElement data)
        {
            return StaticLoad(data, appConfiguration, commandInvoker);
        }

        public static XElement StaticStore(IChemicalTable item, IConfiguration appConfiguration)
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

        public XElement Store(IChemicalTable item)
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
