namespace SafetyProgram.Core.Models
{
    public interface IFormat
    {
        /// <summary>
        /// Get or set the width of the document
        /// </summary>
        string Width { get; }

        string Height { get; }
    }
}
