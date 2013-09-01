using SafetyProgram.Core.IO;

namespace SafetyProgram.Core.Models.Factories
{
    /// <summary>
    /// Defines a standard implementation of a Generator for an IFormat.
    /// Generates a standard A4 format.
    /// </summary>
    public sealed class FormatFactory : IGenerator<IFormat>
    {
        public IFormat CreateNew()
        {
            return new A4Format();
        }
    }
}
