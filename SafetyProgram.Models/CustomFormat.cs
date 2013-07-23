using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Models
{
    /// <summary>
    /// Defines a custom IDocFormat
    /// </summary>
    public sealed class CustomFormat : IFormat
    {
        /// <summary>
        /// Creates a new, custom, IDocFormat with the specified dimensions
        /// </summary>
        /// <param name="width">The width of the document in pixels</param>
        /// <param name="height">The height of the document in pixels</param>
        public CustomFormat(string width, string height)
        {
            Width = width;
            Height = height;
        }

        public string Width
        {
            get;
            private set;
        }

        public string Height
        {
            get;
            private set;
        }
    }
}
