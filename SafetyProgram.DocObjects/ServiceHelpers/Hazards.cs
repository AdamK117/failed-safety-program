using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using SafetyProgram.Models.DataModels;

namespace SafetyProgram.DocObjects.ServiceHelpers
{
    /// <summary>
    /// Helper class for handling HazardModels (Usually Input/Output stuff).
    /// </summary>
    public static class Hazards
    {
        /// <summary>
        /// Writes an XElement containing hazards
        /// </summary>
        /// <param name="hazards">Hazard model data</param>
        /// <returns>An XElement containing hazard data</returns>
        public static XElement WriteXElement(IEnumerable<HazardModel> hazards)
        {
            return (
                new XElement("hazards",
                    from hazard in hazards
                    select new XElement("hazard", hazard.Hazard,
                        new XAttribute("signalword", hazard.SignalWord ?? ""),
                        new XAttribute("symbol", hazard.Symbol ?? "")
                    )
                )
            );
        }

        /// <summary>
        /// Reads in Hazard data from an XElement input
        /// </summary>
        /// <param name="hazardNode"></param>
        /// <returns></returns>
        public static IEnumerable<HazardModel> ReadXElement(XElement hazardNode)
        {
            return (
                from XElement hazard in hazardNode.Elements("hazard")
                select new HazardModel()
                {
                    Hazard = hazard.Value,
                    SignalWord = hazard.Attribute("signalword") == null ? null : hazard.Attribute("signalword").Value,
                    Symbol = hazard.Attribute("symbol") == null ? null : hazard.Attribute("symbol").Value
                }
            );
        }
    }
}
