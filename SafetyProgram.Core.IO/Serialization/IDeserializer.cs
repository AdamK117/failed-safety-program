namespace SafetyProgram.Core.IO
{
    /// <summary>
    /// Defines an interface for a class that may load a class from an in-memory storage type.
    /// </summary>
    /// <typeparam name="TObject">The in-memory representation of the class.</typeparam>
    /// <typeparam name="TSerialized">The in-storage representation of the class.</typeparam>
    public interface IDeserializer<out TObject, in TSerialized>
    {
        /// <summary>
        /// Deserialize the in-storage representation of the class into an in-memory object.
        /// </summary>
        /// <param name="data">The in-storage data to convert.</param>
        /// <returns>The deserialized object.</returns>
        /// <exception cref="System.IO.InvalidDataException">Thrown if the data loaded from storage is invalid in some way.</exception>
        TObject Load(TSerialized data);
    }
}
