using System;
using System.IO;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Static;

namespace SafetyProgram.ModelObjects
{
    public class HazardModelObjectLocalFileFactory
        : IFactory<IHazardModelObject, XElement>
    {
        public static IHazardModelObject StaticCreateNew()
        {
            return new HazardModelObject();
        }

        public IHazardModelObject CreateNew()
        {
            return StaticCreateNew();
        }

        public static IHazardModelObject StaticLoad(XElement data)
        {
            //Variables that are to be loaded
            string loadedHazard, loadedSignalWord, loadedSymbol;

            //Required: Get the hazard statement for this hazard.
            {
                if (!String.IsNullOrWhiteSpace(data.Value))
                {
                    loadedHazard = data.Value;
                }
                else throw new InvalidDataException("No hazard was found inside a hazard statement (every hazard statement must state its hazard).");
            }

            //Optional: Get the hazards signal word. Custom hazards may not have a signal word.
            {
                var signalWordAttr = data.Attribute("signalword");
                loadedSignalWord = (signalWordAttr == null) ? ("") : (signalWordAttr.Value);
            }

            //Optional: Get the symbol associated with the hazard. Not every hazard has a symbol.
            {
                var symbolAttr = data.Attribute("symbol");
                loadedSymbol = (symbolAttr == null) ? ("") : (symbolAttr.Value);
            }

            return new HazardModelObject(loadedHazard, loadedSignalWord, loadedSymbol);
        }

        public IHazardModelObject Load(XElement data)
        {
            return StaticLoad(data);
        }

        public static XElement StaticStore(IHazardModelObject item)
        {
            if (String.IsNullOrWhiteSpace(item.Error))
            {
                return new XElement(XmlNodeNames.HAZARD_MODEL_OBJ,
                    item.Hazard,
                    (item.SignalWord == null) ? (null) : (new XAttribute("signalword", item.SignalWord)),
                    (item.Symbol == null) ? (null) : (new XAttribute("symbol", item.Symbol))
                );
            }
            else throw new InvalidDataException("Errors found during save: " + item.Error);
        }

        public XElement Store(IHazardModelObject item)
        {
            return StaticStore(item);
        }
    }
}
