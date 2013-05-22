using System;
using System.IO;
using System.Xml.Linq;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.ModelObjects
{
    public sealed class CoshhChemicalObjectLocalFileFactory :
        ILocalFileFactory<ICoshhChemicalObject>
    {
        public static ICoshhChemicalObject StaticCreateNew()
        {
            return ModelObjectsPrototypes.CoshhChemicalObject();
        }

        public ICoshhChemicalObject CreateNew()
        {
            return StaticCreateNew();
        }

        public static ICoshhChemicalObject StaticLoad(XElement data)
        {
            //Implemented separately because interfaces do not support statics

            decimal loadedValue;
            string loadedUnit;
            IChemicalModelObject loadedChemical;

            //Required: Get the amount of chemical being used for this CoshhChemical entry.
            {
                var amountElement = data.Element("amount");
                if (amountElement != null)
                {
                    //Parse the amount (decimal) used in this Coshh entry
                    try
                    {
                        loadedValue = decimal.Parse(amountElement.Value);
                    }
                    catch (ArgumentNullException e)
                    {
                        throw new InvalidDataException("Could not process the amount of chemical being used.", e);
                    }
                    catch (FormatException e)
                    {
                        throw new InvalidDataException("Could not parse the amount of chemical into a decimal number", e);
                    }

                    //Required: Get the units for the amount specified
                    {
                        var unitAttribute = amountElement.Attribute("unit");
                        if (unitAttribute != null)
                        {
                            loadedUnit = unitAttribute.Value;
                        }
                        else throw new InvalidDataException("No units were given for the amount of CoshhChemical being used");
                    }
                }
                else throw new InvalidDataException("No amount of the CoshhChemical was found, CoshhChemicals (not raw chemicals) need an amount");
            }

            //Required: Get the chemicals details
            {
                var chemicalElement = data.Element(ChemicalModelObjectLocalFileFactory.XML_IDENTIFIER);
                var chemicalFactory = new ChemicalModelObjectLocalFileFactory();
                if (chemicalElement != null)
                {
                    loadedChemical = chemicalFactory.Load(chemicalElement);
                }
                else throw new InvalidDataException("No chemical was defined for the CoshhChemical");
            }

            return new CoshhChemicalObject(loadedValue, loadedUnit, loadedChemical);
        }

        public ICoshhChemicalObject Load(XElement data)
        {
            return StaticLoad(data);
        }

        public static XElement StaticStore(ICoshhChemicalObject item)
        {
            var chemicalFactory = new ChemicalModelObjectLocalFileFactory();

            if (String.IsNullOrWhiteSpace(item.Error))
            {
                return
                    new XElement(XML_IDENTIFIER,
                        new XElement("amount",
                            item.Value,
                            new XAttribute("unit", item.Unit)
                        ),
                        chemicalFactory.Store(item.Chemical)
                    );
            }
            else throw new InvalidDataException("Errors found during save: " + item.Error);
        }

        public XElement Store(ICoshhChemicalObject item)
        {
            return StaticStore(item);
        }

        public const string XML_IDENTIFIER = "coshhchemical";

        public string XmlIdentifier
        {
            get { return XML_IDENTIFIER; }
        }
    }
}
