using System;
using System.Collections.Generic;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Static;

namespace SafetyProgram.DocumentObjects
{
    public static class DocObjectRegistry
    {
        internal static IDictionary<string, Func<IDocumentObject>> GetRegistry()
        {
            return new Dictionary<string, Func<IDocumentObject>>()
            {
                {XmlNodeNames.CHEMICAL_TABLE_OBJ, () => new ChemicalTable.ChemicalTable()}
            };
        }

        /// <summary>
        /// Returns any IDocumentObjects found when parsing through the children of an XElement
        /// </summary>
        /// <param name="data">Xml data</param>
        /// <returns>Constructed IDocumentObjects</returns>
        public static IEnumerable<IDocumentObject> GetDocObjects(XElement data)
        {
            //A return value holder
            ICollection<IDocumentObject> foundIDocObjects = new List<IDocumentObject>();

            //Registry holder
            IDictionary<string, Func<IDocumentObject>> registry = GetRegistry();            

            //For each Xml name (example: <title>, <chemtable>, etc.)
            foreach(XElement element in data.Elements())
            {
                //See if it's in the registry
                if (registry.ContainsKey(element.Name.LocalName))
                {
                    //And make it if it is.
                    IDocumentObject docObject = registry[element.Name.LocalName]();
                    docObject = docObject.LoadFromXml(element);
                    foundIDocObjects.Add(docObject);
                }
            }
            return foundIDocObjects;
        }
    }
}
