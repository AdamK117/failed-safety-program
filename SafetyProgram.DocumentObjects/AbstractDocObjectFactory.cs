using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Static;

namespace SafetyProgram.DocumentObjects
{
    /// <summary>
    /// Defines an ABSTRACT FACTORY for saving/loading IDocumentObjects
    ///     -Contains a dictionary of ID's (Xml node names) for loading IDocumentObjects from Xml
    ///     -Contains a dictionary of types (of IDocumentObjects) for saving to Xml.
    /// </summary>
    public static class AbstractDocObjectFactory
    {
        private static object syncRoot = new Object();
        private static volatile IDictionary<string, Func<XElement, IDocumentObject>> inputRegistry;
        private static volatile IDictionary<Type, Func<IDocumentObject, XElement>> outputRegistry;

        private static IDictionary<string, Func<XElement, IDocumentObject>> getInputRegistry()
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
                        inputRegistry = new Dictionary<string, Func<XElement, IDocumentObject>>()
                        {
                            {XmlNodeNames.CHEMICAL_TABLE_OBJ, (element) => ChemicalTable.ChemicalTableLocalFileFactory.StaticLoad(element) }
                        };
                    }
                }
            }
            return inputRegistry;           
        }

        private static IDictionary<Type, Func<IDocumentObject, XElement>> getOutputRegistry()
        {
            if (outputRegistry == null)
            {
                lock (syncRoot)
                {
                    if (outputRegistry == null)
                    {
                        outputRegistry = new Dictionary<Type, Func<IDocumentObject, XElement>>()
                            {
                                {typeof(ChemicalTable.ChemicalTable), (docobj) => ChemicalTable.ChemicalTableLocalFileFactory.StaticStore(docobj as ChemicalTable.ChemicalTable) }
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
        public static IDocumentObject GetDocObject(XElement data)
        {
            var registry = getInputRegistry();
            var nodeName = data.Name.LocalName;

            if (registry.ContainsKey(nodeName))
            {
                return registry[nodeName](data);
            }
            else return null;
        }

        /// <summary>
        /// Attempts to save an IDocumentObject to an Xml (if a factory can be found for the IDocumentObject)
        /// </summary>
        /// <param name="docObject">The IDocumentObject to save to the Xml node</param>
        /// <returns></returns>
        public static XElement SaveDocObject(IDocumentObject docObject)
        {
            var registry = getOutputRegistry();
            var typeArg = docObject.GetType();

            if (registry.ContainsKey(typeArg))
            {
                return registry[typeArg](docObject);
            }
            else return null;
        }

        /// <summary>
        /// Iterates through all the nodes contained within an Xml node, returning any IDocumentObjects that were found.
        /// </summary>
        /// <param name="data">An Xml node containing IDocumentObject children</param>
        /// <returns>Loaded IDocumentObjects</returns>
        public static IEnumerable<IDocumentObject> GetDocObjects(XElement data)
        {
            foreach (XElement element in data.Elements())
            {
                yield return GetDocObject(element);
            }
        }

        /// <summary>
        /// Attempts to save each IDocumentObject into an XElement
        /// </summary>
        /// <param name="docObjects"></param>
        /// <returns></returns>
        public static IEnumerable<XElement> SaveDocObjects(IEnumerable<IDocumentObject> docObjects)
        {
            foreach (IDocumentObject docObject in docObjects)
            {
                yield return SaveDocObject(docObject);
            }
        }
    }
}
