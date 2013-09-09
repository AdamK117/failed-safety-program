using System;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines an IDoc. A document that conforms to a certain format (A4, A3, etc.) and contains objects (ChemicalTables, Headers, TextBoxes, etc.)
    /// </summary>
    public interface IDocument : 
        IApplicationModel,
        IHasMany<IDocumentObject>
    {
        /// <summary>
        /// Gets the IFormat associated with this IDoc.
        /// </summary>
        /// <example>A4, A3, Poster, Banner, etc.</example>
        IFormat Format { get; set; }

        /// <summary>
        /// Occurs when the format of the IDoc changes.
        /// </summary>
        event EventHandler<
            GenericPropertyChangedEventArg<
                IFormat>> FormatChanged;
    }
}
