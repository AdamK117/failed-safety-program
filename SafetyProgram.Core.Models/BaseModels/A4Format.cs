using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines an A4 print format.
    /// </summary>
    public class A4Format : IFormat
    {
        /// <summary>
        /// Get the width of the print format.
        /// </summary>
        public string Width
        {
            get { return "630"; }
        }

        /// <summary>
        /// Get the height of the print format.
        /// </summary>
        public string Height
        {
            get { return "891"; }
        }

        public string Identifier
        {
            get { return ModelIdentifiers.FORMAT_IDENTIFIER; }
        }
    }
}
