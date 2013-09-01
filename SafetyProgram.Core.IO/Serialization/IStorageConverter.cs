namespace SafetyProgram.Core.IO
{
    /// <summary>
    /// Defines a general storage interface for converting between an 
    /// in-memory and in-storage representation of a class.
    /// </summary>
    /// <typeparam name="TObject">The in-memory object.</typeparam>
    /// <typeparam name="TSerialized">The in-storage representation of the class.</typeparam>
    public interface IStorageConverter<TObject, TSerialized> :
        ISerializer<TSerialized, TObject>,
        IDeserializer<TObject, TSerialized>
    { }
}
