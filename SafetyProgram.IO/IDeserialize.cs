namespace SafetyProgram.IO
{
    /// <summary>
    /// Defines a general storage (loading from disk, db, etc.) interface.
    /// </summary>
    /// <typeparam name="TObject">The type of data when it is held in memory (e.g. SomeType)</typeparam>
    /// <typeparam name="TSerialized">The type of the data when it is held in storage (e.g. XElement, Db)</typeparam>
    public interface IDeserialize<out TObject, in TSerialized>
    {
        /// <summary>
        /// Convert the data from the format held on disk to the format held in memory.
        /// </summary>
        /// <param name="data">The data to convert.</param>
        /// <returns></returns>
        /// <exception cref="System.IO.InvalidDataException">Thrown if the data loaded from storage is invalid in some way.</exception>
        TObject Load(TSerialized data);
    }
}
