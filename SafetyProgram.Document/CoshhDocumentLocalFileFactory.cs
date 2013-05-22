using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Body;

namespace SafetyProgram.Document
{
    public class CoshhDocumentLocalFileFactory
        : ILocalFileFactory<ICoshhDocument>
    {
        private readonly ILocalFileFactory<IDocumentObject> docObjFactory;
        private readonly IConfiguration appConfiguration;
        private readonly ICommandInvoker commandInvoker;

        public CoshhDocumentLocalFileFactory(
            ILocalFileFactory<IDocumentObject> docObjFactory,
            IConfiguration appConfiguration, 
            ICommandInvoker commandInvoker
            )
        {
            if (
                docObjFactory != null &&
                appConfiguration != null &&
                commandInvoker != null
                )
            {
                this.docObjFactory = docObjFactory;
                this.appConfiguration = appConfiguration;
                this.commandInvoker = commandInvoker;
            }
            else throw new ArgumentNullException();
        }

        public static ICoshhDocument StaticCreateNew(IConfiguration appConfiguration, ICommandInvoker commandInvoker)
        {
            return CoshhDocumentDefault.DefaultCoshhDocument(appConfiguration, commandInvoker);
        }

        public ICoshhDocument CreateNew()
        {
            return StaticCreateNew(appConfiguration, commandInvoker);
        }

        public static ICoshhDocument StaticLoad(
            XElement data, 
            ILocalFileFactory<IDocumentObject> docObjFactory, 
            IConfiguration appConfiguration, 
            ICommandInvoker commandInvoker
            )
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
                        loadedTitle = CoshhDocumentDefault.DefaultTitle;
                    }
                }

                //Optional: Get the format of the document
                loadedFormat = CoshhDocumentDefault.DefaultFormat();

                //Required: Get the body of the document
                loadedBody = new CoshhDocumentBody(
                    from iDocObject in data.Elements()
                    select docObjFactory.Load(iDocObject)
                    );
            }
            else throw new InvalidDataException("No CoshhDocument root could be found (<coshh></coshh>)");

            return new CoshhDocument(
                appConfiguration, 
                loadedTitle, 
                loadedFormat, 
                loadedBody,
                CoshhDocumentDefault.CommandsConstructor(commandInvoker),
                CoshhDocumentDefault.ContextMenuConstructor,
                CoshhDocumentDefault.RibbonTabsConstructor,
                CoshhDocumentDefault.ViewConstructor
                );
        }

        public ICoshhDocument Load(XElement data)
        {
            return StaticLoad(data, docObjFactory, appConfiguration, commandInvoker);
        }

        public static XElement StaticStore(ICoshhDocument item, ILocalFileFactory<IDocumentObject> docObjFactory, IConfiguration appConfiguration)
        {
            XElement xDoc = new XElement("coshh",
                from docObj in item.Body.Items
                select docObjFactory.Store(docObj)
            );

            return xDoc;
        }

        public XElement Store(ICoshhDocument item)
        {
            return StaticStore(item, docObjFactory, appConfiguration);
        }

        public const string XML_IDENTIFIER = "coshhdocument";

        public string XmlIdentifier
        {
            get { return XML_IDENTIFIER; }
        }
    }
}
