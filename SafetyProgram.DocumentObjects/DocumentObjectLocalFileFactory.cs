using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs;

namespace SafetyProgram.DocumentObjects
{
    public sealed class DocumentObjectLocalFileFactory : 
        ILocalFileFactory<IDocumentObject>
    {
        private readonly IConfiguration appConfiguration;
        private readonly ICommandInvoker commandInvoker;
        private readonly IDictionary<string, ILoader<IDocumentObject, XElement>> creationFactories;
        private readonly IDictionary<Type, Func<IDocumentObject, XElement>> outputFactories;

        public DocumentObjectLocalFileFactory(
            IConfiguration appConfiguration, 
            ICommandInvoker commandInvoker
            )
        {
            if (appConfiguration != null && commandInvoker != null)
            {
                this.appConfiguration = appConfiguration;
                this.commandInvoker = commandInvoker;
            }
            else throw new ArgumentNullException();

            //Registry of available docObjects
            creationFactories = new Dictionary<string, ILoader<IDocumentObject, XElement>>()
            {
                {
                    ChemicalTableLocalFileFactory.XML_IDENTIFIER,
                    new ChemicalTableLocalFileFactory(appConfiguration, commandInvoker)
                }
            };

            //Registry of available output methods for docObjects
            outputFactories = new Dictionary<Type, Func<IDocumentObject, XElement>>()
            {
                {
                    typeof(ChemicalTable),
                    (docObject) => new ChemicalTableLocalFileFactory(appConfiguration, commandInvoker).Store((ChemicalTable)docObject)
                }
            };
        }

        public const string XML_IDENTIFIER = "documentobject";

        public string XmlIdentifier
        {
            get 
            {
                return XML_IDENTIFIER; 
            }
        }

        public XElement Store(IDocumentObject item)
        {
            var typeArg = item.GetType();

            if (outputFactories.ContainsKey(typeArg))
            {
                return outputFactories[typeArg](item);
            }
            else throw new InvalidDataException();
        }

        public IDocumentObject CreateNew()
        {
            throw new NotImplementedException();
        }

        public IDocumentObject Load(XElement data)
        {
            var nodeName = data.Name.LocalName;

            if (creationFactories.ContainsKey(nodeName))
            {
                return creationFactories[nodeName].Load(data);
            }
            else throw new InvalidDataException();
        }
    }
}
