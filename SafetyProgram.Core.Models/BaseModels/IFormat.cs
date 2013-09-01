namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines a print format.
    /// </summary>
    public interface IFormat
    {
        /// <summary>
        /// Get or the width of the document
        /// </summary>
        string Width { get; }

        /// <summary>
        /// Get the height of the document.
        /// </summary>
        string Height { get; }
    }
}
