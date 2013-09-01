namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines an interface for a ICoshhChemical. This is both a chemical and 
    /// the amount of that chemical used.
    /// </summary>
    /// <example>Acetonitrile, 30 mgs</example>
    public interface ICoshhChemical : IDocumentObject
    {
        /// <summary>
        /// Get the IChemical for this ICoshhChemical entry.
        /// </summary>
        IChemical Chemical { get; }

        /// <summary>
        /// Get the quantity of the IChemical used in this ICoshhChemical entry.
        /// </summary>
        IQuantity Amount { get; }
    }
}
