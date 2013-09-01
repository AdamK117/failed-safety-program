using System.Collections.Generic;

namespace SafetyProgram.Core.IO
{
    /// <summary>
    /// Defines a general storage interaface for local files. This is an extended version of
    /// an IStorageConverter that also adds support for holding the file extension (e.g. '.xml')
    /// and file description of the file to be converted (e.g. 'Xml File Format').
    /// </summary>
    /// <typeparam name="TObject">The in-memory class representation.</typeparam>
    /// <typeparam name="TSerialized">The in-storage class representation.</typeparam>
    public interface ILocalStorageConverter<TObject, TSerialized> : 
        IStorageConverter<TObject, TSerialized>
    {
        /// <summary>
        /// Get the extensions associated with the converter.
        /// </summary>
        /// <example>['xml','html','xhtml']</example>
        IEnumerable<string> Extensions { get; }

        /// <summary>
        /// Get a description of the type of the in-memory data.
        /// </summary>
        /// <example>XML file format.</example>
        string FileDescription { get; }
    }
}
