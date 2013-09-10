using System;
using System.IO;
using System.Xml.Linq;
using SafetyProgram.Core.IO;

namespace SafetyProgram.Core.Models.Serialization
{
    public sealed class CoshhChemicalXml : IStorageConverter<ICoshhChemical, XElement>
    {
        /// <summary>
        /// Serialize a coshh chemical into an XML format.
        /// </summary>
        /// <param name="data">The coshh chemical to serialize.</param>
        /// <returns>The serialized coshh chemical.</returns>
        public XElement Store(ICoshhChemical data)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deserialize a coshh chemical stored in an XML format.
        /// </summary>
        /// <param name="data">The coshh chemical in an XML format.</param>
        /// <returns>The deserialized coshh chemical object.</returns>
        public ICoshhChemical Load(XElement data)
        {
            decimal loadedValue;
            string loadedUnit;
            IChemical loadedChemical;

            // Required: Get the amount of chemical being used for the entry
            {
                var amountElement = data.Element("amount");
                if (amountElement != null)
                {
                    try
                    {
                        loadedValue = decimal.Parse(amountElement.Value);
                    }
                    catch (ArgumentNullException e)
                    {
                        throw new InvalidDataException("Could not process the amount of chemical being used ", e);
                    }
                    catch (FormatException e)
                    {
                        throw new InvalidDataException("Could not parse the amount specified into a decimal number", e);
                    }
                }
                else throw new InvalidDataException("No amount specified for the coshh chemical");

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

            // Required: Get the chemicals details.
            {
                var chemicalElement = data.Element("chemical");
                var chemicalFactory = new ChemicalXml();
                if (chemicalElement != null)
                {
                    loadedChemical = chemicalFactory.Load(chemicalElement);
                }
                else throw new InvalidDataException("No chemical was defined for the CoshhChemical");
            }

            var quantity = new Quantity(loadedValue, loadedUnit);
            return new CoshhChemical(loadedChemical, quantity);
        }
    }
}
