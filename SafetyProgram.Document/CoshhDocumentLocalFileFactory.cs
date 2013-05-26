using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.DocumentFormats;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Commands;
using SafetyProgram.Document.ContextMenus;
using SafetyProgram.Document.Ribbons;

namespace SafetyProgram.Document
{
    public class CoshhDocumentLocalFileFactory
        : ILocalFileFactory<ICoshhDocument>
    {
        private readonly ILocalFileFactory<IDocumentObject> docObjFactory;
        private readonly IConfiguration appConfiguration;
        private readonly ICommandInvoker commandInvoker;

        public CoshhDocumentLocalFileFactory(IConfiguration appConfiguration, 
            ICommandInvoker commandInvoker, 
            ILocalFileFactory<IDocumentObject> documentObjectFactory)
        {
            Helpers.NullCheck(documentObjectFactory, appConfiguration, commandInvoker);

            this.docObjFactory = documentObjectFactory;
            this.appConfiguration = appConfiguration;
            this.commandInvoker = commandInvoker;
        }

        public const string XML_IDENTIFIER = "coshhdocument";
        public string XmlIdentifier
        {
            get { return XML_IDENTIFIER; }
        }

        public ICoshhDocument CreateNew()
        {
            var documentBody = new CoshhDocumentBody(
                new ObservableCollection<IDocumentObject>()
            );

            var documentCommands = new DocumentICommands(
                documentBody,
                appConfiguration,
                commandInvoker
            );

            var documentFormat = new Holder<IFormat>(
                new A4Format()
            );

            var documentRibbonTabs = new ObservableCollection<RibbonTabItem>()
            {
                new CoshhDocumentRibbonTabView(
                    new CoshhDocumentRibbonTabViewModel(
                        documentCommands
                    )
                )
            };

            //Construct the new CoshhDocument
            return new CoshhDocument(
                new CoshhDocumentView(
                    new CoshhDocumentViewModel(
                        documentFormat,
                        new DocumentContextMenuView(
                            new DocumentContextMenuViewModel(
                                documentCommands
                            )
                        ),
                        documentBody,
                        documentCommands.Hotkeys
                    )
                ),
                documentBody,
                documentFormat,
                documentRibbonTabs
            );
        }

        public ICoshhDocument Load(XElement data)
        {
            string loadedTitle;
            IFormat loadedFormat;
            IDocumentBody loadedBody;

            if (data != null)
            {
                //Optional: Get the title of the document
                {
                    var titleAttr = data.Attribute("title");
                    if (titleAttr != null)
                    {
                        loadedTitle = titleAttr.Value;
                    }
                    else
                    {
                        Debug.Write("WARNING: When loading a CoshhDocument a title could not be found, set to default");
                        loadedTitle = "SomeDefaultTitle";
                    }
                }

                //Optional: Get the format of the document
                loadedFormat = new A4Format();

                //Required: Get the body of the document
                loadedBody = new CoshhDocumentBody(
                    new ObservableCollection<IDocumentObject>(
                        from iDocObject in data.Elements()
                        select docObjFactory.Load(iDocObject)
                    )
                );
            }
            else throw new InvalidDataException("No CoshhDocument root could be found (<coshh></coshh>)");

            var documentCommands = new DocumentICommands(
                loadedBody,
                appConfiguration,
                commandInvoker
            );

            var documentRibbonTabs = new ObservableCollection<RibbonTabItem>()
            {
                new CoshhDocumentRibbonTabView(
                    new CoshhDocumentRibbonTabViewModel(
                        documentCommands
                    )
                )
            };

            var docFormat = new Holder<IFormat>(loadedFormat);

            return new CoshhDocument(
                new CoshhDocumentView(
                    new CoshhDocumentViewModel(
                        docFormat,
                        new DocumentContextMenuView(
                            new DocumentContextMenuViewModel(
                                documentCommands
                            )
                        ),
                        loadedBody,
                        documentCommands.Hotkeys
                    )
                ),
                loadedBody,
                docFormat,
                documentRibbonTabs
            );
        }

        public XElement Store(ICoshhDocument data)
        {
            return new XElement(XML_IDENTIFIER,
                from docObj in data.Body.Items
                select docObjFactory.Store(docObj)
            );
        }
    }
}
