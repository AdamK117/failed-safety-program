using System;
using System.Collections.ObjectModel;
using SafetyProgram.Base;

namespace SafetyProgram.Core.Models
{
    /// <summary>
    /// Defines an implementation of IDoc. A class that holds document information (content, print format, etc.)
    /// </summary>
    public sealed class Document : IDocument
    {
        /// <summary>
        /// Construct a new instance of Doc. A class that holds the information associated with a document.
        /// </summary>
        /// <param name="items">The items (content) of the Doc.</param>
        /// <param name="format">The format associated with the Doc.</param>
        public Document(ObservableCollection<IDocumentObject> items, IFormat format)
        {
            Helpers.NullCheck(items, format);

            this.Content = items;
            this.format = format;
        }

        private IFormat format;

        /// <summary>
        /// Get the IFormat associated with the Doc.
        /// </summary>
        public IFormat Format
        {
            get { return format; }
            set 
            {
                if (value != null)
                {
                    format = value;
                    FormatChanged.Raise(this, format);
                }
                else throw new ArgumentNullException("Cannot set a Doc's format to null!");
            }
        }

        /// <summary>
        /// Occurs when the Format of the Doc changes.
        /// </summary>
        public event EventHandler<
            GenericPropertyChangedEventArg<
                IFormat>> FormatChanged;

        public ObservableCollection<IDocumentObject> Content { get; private set; }

        public string Identifier
        {
            get { return ModelIdentifiers.DOCUMENT_IDENTIFIER; }
        }
    }
}
