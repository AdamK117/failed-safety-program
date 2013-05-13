using System.Diagnostics;
using System.IO;
using System.Xml.Linq;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Body;
using SafetyProgram.DocumentObjects;

namespace SafetyProgram.Document
{
    public class CoshhDocumentLocalFileFactory
        : ILocalFileFactory<CoshhDocument>
    {
        private readonly IConfiguration appConfiguration;

        public CoshhDocumentLocalFileFactory(IConfiguration appConfiguration)
        {
            this.appConfiguration = appConfiguration;
        }

        public static CoshhDocument StaticCreateNew(IConfiguration appConfiguration)
        {
            return CoshhDocumentDefault.DefaultCoshhDocument(appConfiguration);
        }

        public CoshhDocument CreateNew()
        {
            return StaticCreateNew(appConfiguration);
        }

        public static CoshhDocument StaticLoad(XElement data, IConfiguration appConfiguration)
        {
            string loadedTitle;
            IDocFormat loadedFormat;
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
                    DocumentObjectLocalFileFactory.GetDocObjects(data, appConfiguration)
                );
            }
            else throw new InvalidDataException("No CoshhDocument root could be found (<coshh></coshh>)");

            return new CoshhDocument(
                appConfiguration, 
                loadedTitle, 
                loadedFormat, 
                loadedBody,
                CoshhDocumentDefault.CommandsConstructor,
                CoshhDocumentDefault.ContextMenuConstructor,
                CoshhDocumentDefault.RibbonTabsConstructor,
                CoshhDocumentDefault.ViewConstructor
                );
        }

        public CoshhDocument Load(XElement data)
        {
            return StaticLoad(data, appConfiguration);
        }

        public static XElement StaticStore(CoshhDocument item, IConfiguration appConfiguration)
        {
            XElement xDoc = new XElement("coshh",
                DocumentObjectLocalFileFactory.SaveDocObjects(item.Body.Items, appConfiguration)
            );

            return xDoc;
        }

        public XElement Store(CoshhDocument item)
        {
            return StaticStore(item, appConfiguration);
        }

        public const string XML_IDENTIFIER = "coshhdocument";

        public string XmlIdentifier
        {
            get { return XML_IDENTIFIER; }
        }
    }
}
