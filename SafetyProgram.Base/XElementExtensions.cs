using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Xml.Linq;

namespace SafetyProgram.Base
{
    public static class XElementExtensions
    {
        public static string ExtractStrict(this XElement xmlElement, string errorMessage)
        {
            if (xmlElement != null)
            {
                Debug.Assert(xmlElement.Value.Length > 0, "Empty xml tags parsed");
                return xmlElement.Value;
            }
            else throw new InvalidDataException(errorMessage);
        }

        public static string Extract(this XElement xmlElement, string defaultValue)
        {
            if (xmlElement != null)
            {
                Debug.Assert(xmlElement.Value.Length > 0, "Empty xml tags parsed");
                return xmlElement.Value;
            }
            else return defaultValue;
        }

        public static IEnumerable<XElement> ExtractElements(this XElement xmlElement, string elementsName)
        {
            if (xmlElement != null)
            {
                return xmlElement.Elements(elementsName);
            }
            else return new List<XElement>();
        }
    }
}
