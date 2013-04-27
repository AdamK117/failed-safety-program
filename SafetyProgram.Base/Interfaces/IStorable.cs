using System.ComponentModel;
using System.Xml.Linq;

namespace SafetyProgram.Base.Interfaces
{
    /// <summary>
    /// Defines an object that is storable.
    /// -It may be loaded/saved in an XmlFormat
    /// This interface is subject to extension with enhancements in storage (databases, etc.)
    /// </summary>
    public interface IStorable : IDataErrorInfo
    {
        /// <summary>
        /// Loads data into the IStorable from an XElement source. Generates Debug messages for data warnings.
        /// </summary>
        /// <param name="data">Raw, Xml format, data</param>
        /// <exception cref="System.IO.InvalidDataException">Thrown when the supplied data is invalid and cannot be loaded.</exception>
        void LoadData(XElement data);

        /// <summary>
        /// Writes the IStorable to an XElement for saving.
        /// </summary>
        /// <returns>The IStorable's data in an XElement format</returns>
        /// <exception cref="System.IO.InvalidDataException">Thrown if the data currently contained in the IStorable is invalid. Uses IDataErrorInfo checks</exception>
        XElement WriteToXElement();
    }
}
