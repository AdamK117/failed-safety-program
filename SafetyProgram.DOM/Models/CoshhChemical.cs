using SafetyProgram.Base;

namespace SafetyProgram.DOM.Models
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

            this.chemical = chemical;
            this.amount = amount;
        }

        private readonly IChemical chemical;

        /// <summary>
        /// Gets the IChemical associated with this CoshhChemical entry.
        /// </summary>
        public IChemical Chemical
        {
            get { return chemical; }
        }

        private readonly IQuantity amount;

        /// <summary>
        /// Gets the amount of Chemical being used.
        /// </summary>
        /// <example>10 mgs</example>
        public IQuantity Amount
        {
            get { return amount; }
        }

        /// <summary>
        /// Gets the unique identifer associated with this IDocObject.
        /// </summary>
        public string Identifier
        {
            get { return ObjIdentifiers.COSHH_CHEMICAL_IDENTIFIER; }
        }
    }
}
