using System;
using System.Collections.ObjectModel;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Models
{
    /// <summary>
    /// Defines an IDoc. A document that conforms to a certain format (A4, A3, etc.) and contains objects (ChemicalTables, Headers, TextBoxes, etc.)
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// Gets the content of this IDoc.
        /// </summary>
        ObservableCollection<IDocumentObject> Items { get; }

        /// <summary>
        /// Gets the IFormat associated with this IDoc.
        /// </summary>
        /// <example>A4, A3, Poster, Banner, etc.</example>
        IFormat Format { get; }

        /// <summary>
        /// Changes the IDocs format to the new format specified.
        /// </summary>
        /// <param name="newFormat">The new document format, cannot be null.</param>
        void ChangeFormat(IFormat newFormat);

        /// <summary>
        /// Occurs when the format of the IDoc changes.
        /// </summary>
        event EventHandler<GenericPropertyChangedEventArg<IFormat>> FormatChanged;
    }
}
