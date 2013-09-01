namespace SafetyProgram.Core.IO
{
    /// <summary>
    /// Defines a general storage (disk, db, etc.) interface.
    /// </summary>
    /// <typeparam name="TSerialized">The type of the data when it is stored. (e.g. Xml)</typeparam>
    /// <typeparam name="TObject">The type of the data when it is loaded into memory (e.g. SomeType)</typeparam>
    public interface ISerializer<out TSerialized, in TObject>
    {
        /// <summary>
        /// Convert the data from the format held in memory to the stored format.
        /// </summary>
        /// <param name="data">The data to convert.</param>
        /// <returns>The converted data</returns>
        /// <exception cref="System.IO.InvalidDataException">Thrown if the in-memory data is invalid for saving.</exception>
        TSerialized Store(TObject data);
    }
}
