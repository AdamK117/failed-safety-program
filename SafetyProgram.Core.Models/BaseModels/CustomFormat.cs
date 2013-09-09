namespace SafetyProgram.Core.Models
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

        /// <summary>
        /// Get the width of the print format.
        /// </summary>
        public string Width { get; private set; }

        /// <summary>
        /// Get the height of the print format.
        /// </summary>
        public string Height { get; private set; }

        public string Identifier
        {
            get { return ModelIdentifiers.FORMAT_IDENTIFIER; }
        }
    }
}
