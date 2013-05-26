using System.Collections.ObjectModel;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;
using SafetyProgram.DocumentObjects.ChemicalTableNs.ContextMenus;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    public sealed class ChemicalTableLocalFileFactory
        : ILocalFileFactory<IChemicalTable>
    {
        private readonly IConfiguration appConfiguration;
        private readonly ICommandInvoker commandInvoker;

        public ChemicalTableLocalFileFactory(IConfiguration appConfiguration, ICommandInvoker commandInvoker)
        {
            Helpers.NullCheck(appConfiguration, commandInvoker);

            this.appConfiguration = appConfiguration;
            this.commandInvoker = commandInvoker;
        }

        public IChemicalTable CreateNew()
        {
            return ChemicalTableFacade.ChemicalTable(appConfiguration, commandInvoker);
        }

        public IChemicalTable Load(XElement data)
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

            var headerHolder = new Holder<string>(loadedHeader);
            var selectedChemicals = new ObservableCollection<ICoshhChemicalObject>();
            var tableCommands = new ChemicalTableCommands(
                selectedChemicals,
                loadedChemicals,
                commandInvoker
            );

            return new ChemicalTable(
                headerHolder,
                loadedChemicals,
                new ChemicalTableRibbonView(
                    new ChemicalTableRibbonTabViewModel(
                        appConfiguration,
                        tableCommands
                    )
                ),
                new ChemicalTableView(
                    new ChemicalTableViewModel(
                        headerHolder,
                        new ChemicalTableContextMenuView(
                            new ChemicalTableContextMenuViewModel(
                                tableCommands
                            )
                        ),
                        loadedChemicals,
                        selectedChemicals,
                        tableCommands.Hotkeys
                    )
                )
            );
        }

        public XElement Store(IChemicalTable item)
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

        public const string XML_IDENTIFIER = "chemicaltable";

        public string XmlIdentifier
        {
            get { return XML_IDENTIFIER; }
        }
    }
}
