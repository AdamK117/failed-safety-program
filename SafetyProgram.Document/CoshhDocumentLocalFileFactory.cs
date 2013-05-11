using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Base;
using SafetyProgram.Base.DocumentFormats;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Body;
using SafetyProgram.DocumentObjects;

namespace SafetyProgram.Document
{
    public class CoshhDocumentLocalFileFactory
        : IFactory<CoshhDocument, XElement>
    {
        public static CoshhDocument StaticCreateNew()
        {
            return new CoshhDocument();
        }

        public CoshhDocument CreateNew()
        {
            return StaticCreateNew();
        }

        public static CoshhDocument StaticLoad(XElement data)
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
                        loadedTitle = "Untitled CoshhDocument";
                    }
                }

                //Optional: Get the format of the document
                loadedFormat = new A4DocFormat();

                //Required: Get the body of the document
                loadedBody = new CoshhDocumentBody(
                    AbstractDocObjectFactory.GetDocObjects(data)
                );
            }
            else throw new InvalidDataException("No CoshhDocument root could be found (<coshh></coshh>)");

            return new CoshhDocument(loadedTitle, loadedFormat, loadedBody);
        }

        public CoshhDocument Load(XElement data)
        {
            return StaticLoad(data);
        }

        public static XElement StaticStore(CoshhDocument item)
        {
            //TODO: Validation checks

            XElement xDoc = new XElement("coshh",
                from docObject in item.Body.Items
                select AbstractDocObjectFactory.SaveDocObject(docObject)
            );

            return xDoc;
        }

        public XElement Store(CoshhDocument item)
        {
            return StaticStore(item);
        }
    }
}
