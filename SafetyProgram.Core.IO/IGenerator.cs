namespace SafetyProgram.Core.IO
{
    /// <summary>
    /// Defines an interface for a class that may generate an object with 
    /// no additional paramaters. These may act as facades for classes with 
    /// complex constructors.
    /// </summary>
    public interface IGenerator<T>
    {
        /// <summary>
        /// Create a new object from this generator.
        /// </summary>
        /// <returns>A newly created object.</returns>
        T CreateNew();
    }
}
