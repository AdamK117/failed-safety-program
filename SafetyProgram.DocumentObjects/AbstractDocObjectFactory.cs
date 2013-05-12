using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs;

namespace SafetyProgram.DocumentObjects
{
    /// <summary>
    /// Defines an ABSTRACT FACTORY for saving/loading IDocumentObjects
    ///     -Contains a dictionary of ID's (Xml node names) for loading IDocumentObjects from Xml
    ///     -Contains a dictionary of types (of IDocumentObjects) for saving to Xml.
    /// </summary>
    public static class DocumentObjectLocalFileFactory
    {
        private static object syncRoot = new Object();
        private static volatile IDictionary<string, Func<XElement, IConfiguration, IDocumentObject>> inputRegistry;
        private static volatile IDictionary<Type, Func<IDocumentObject, IConfiguration, XElement>> outputRegistry;

        private static IDictionary<string, Func<XElement, IConfiguration, IDocumentObject>> getInputRegistry()
        {
            //If the registry hasn't been made yet
            if (inputRegistry == null)
            {
                //Lock multithreading to prevent multiple creations of an instance
                lock (syncRoot)
                {
                    //Doublecheck
                    if (inputRegistry == null)
                    {
                        //Make the instance
                        inputRegistry = new Dictionary<string, Func<XElement, IConfiguration, IDocumentObject>>()
                        {
                            {
                                ChemicalTableLocalFileFactory.XML_IDENTIFIER, 
                                (element, appConf) => ChemicalTableLocalFileFactory.StaticLoad(element, appConf) 
                            }
                        };
                    }
                }
            }
            return inputRegistry;           
        }

        private static IDictionary<Type, Func<IDocumentObject, IConfiguration, XElement>> getOutputRegistry()
        {
            if (outputRegistry == null)
            {
                lock (syncRoot)
                {
                    if (outputRegistry == null)
                    {
                        outputRegistry = new Dictionary<Type, Func<IDocumentObject, IConfiguration, XElement>>()
                            {
                                {
                                    typeof(ChemicalTable), 
                                    (docobj, appConf) => ChemicalTableLocalFileFactory.StaticStore((ChemicalTable)docobj, appConf) 
                                }
                            };
                    }
                }
            }
            return outputRegistry;
        }

        /// <summary>
        /// Tries to get a IDocumentObject from the provided Xml node
        /// </summary>
        /// <param name="data">An IDocumentObject Xml node</param>
        /// <returns>A loaded DocumentObject</returns>
        private static IDocumentObject getDocObject(XElement data, IConfiguration appConfiguration)
        {
            var registry = getInputRegistry();
            var nodeName = data.Name.LocalName;

            if (registry.ContainsKey(nodeName))
            {
                return registry[nodeName](data, appConfiguration);
            }
            else return null;
        }

        /// <summary>
        /// Attempts to save an IDocumentObject to an Xml (if a factory can be found for the IDocumentObject)
        /// </summary>
        /// <param name="docObject">The IDocumentObject to save to the Xml node</param>
        /// <returns></returns>
        private static XElement saveDocObject(IDocumentObject docObject, IConfiguration appConfiguration)
        {
            var registry = getOutputRegistry();
            var typeArg = docObject.GetType();

            if (registry.ContainsKey(typeArg))
            {
                return registry[typeArg](docObject, appConfiguration);
            }
            else return null;
        }

        /// <summary>
        /// Iterates through all the nodes contained within an Xml node, returning any IDocumentObjects that were found.
        /// </summary>
        /// <param name="data">An Xml node containing IDocumentObject children</param>
        /// <returns>Loaded IDocumentObjects</returns>
        public static IEnumerable<IDocumentObject> GetDocObjects(XElement data, IConfiguration appConfiguration)
        {
            foreach (XElement element in data.Elements())
            {
                yield return getDocObject(element, appConfiguration);
            }
        }

        /// <summary>
        /// Attempts to save each IDocumentObject into an XElement
        /// </summary>
        /// <param name="docObjects"></param>
        /// <returns></returns>
        public static IEnumerable<XElement> SaveDocObjects(IEnumerable<IDocumentObject> docObjects, IConfiguration appConfiguration)
        {
            foreach (IDocumentObject docObject in docObjects)
            {
                yield return saveDocObject(docObject, appConfiguration);
            }
        }
    }
}
