using SafetyProgram.Base;

namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines an implementation of ICoshhChemical. A chemical that appears in a Coshh form.
    /// </summary>
    public sealed class CoshhChemical : ICoshhChemical
    {
        /// <summary>
        /// Construct an instance of the CoshhChemical class. A class that defines a CoshhChemical entry.
        /// </summary>
        /// <param name="chemical">The IChemical associated with this CoshhChemical.</param>
        /// <param name="amount">The amount of Chemical being used.</param>
        public CoshhChemical(IChemical chemical, IQuantity amount)
        {
            Helpers.NullCheck(chemical, amount);

            this.Chemical = chemical;
            this.Amount = amount;
        }

        /// <summary>
        /// Gets the IChemical associated with this CoshhChemical entry.
        /// </summary>
        public IChemical Chemical { get; private set; }

        /// <summary>
        /// Gets the amount of Chemical being used.
        /// </summary>
        /// <example>10 mgs</example>
        public IQuantity Amount { get; private set; }

        /// <summary>
        /// Gets the unique identifer associated with this IDocObject.
        /// </summary>
        public string Identifier
        {
            get { return ModelIdentifiers.COSHH_CHEMICAL_IDENTIFIER; }
        }
    }
}
