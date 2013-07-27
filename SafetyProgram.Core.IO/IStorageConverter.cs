namespace SafetyProgram.IO
{
    /// <summary>
    /// Defines a general storage interface for converting between an in memory representation of data and its storage type representation.
    /// </summary>
    /// <typeparam name="TObject">The type of the data when held for program use (e.g. SomeType)</typeparam>
    /// <typeparam name="TSerialized">The type of the data when being stored (e.g. XElement)</typeparam>
    public interface IStorageConverter<TObject, TSerialized> :
        ISerialize<TSerialized, TObject>,
        IDeserialize<TObject, TSerialized>
    {
    }
}
