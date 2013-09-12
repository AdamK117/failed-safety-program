using System.Collections.ObjectModel;
using System.ComponentModel;
using SafetyProgram.Base;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.DocumentObject;

namespace SafetyProgram.UI.Document
{
    /// <summary>
    /// Defines a standard implementation of an IDocumentViewmodel.
    /// </summary>
    internal sealed class DocumentViewModel : IDocumentViewModel
    {
        private readonly IDocument document;

        /// <summary>
        /// Construct an instance of a document viewmodel.
        /// </summary>
        /// <param name="document">The underlying document model.</param>
        /// <param name="documentCommands">Commands that act on the document model.</param>
        public DocumentViewModel(IDocument document,
            ReadOnlyObservableCollection<IDocumentObjectUiController> documentObjects)
        {
            Helpers.NullCheck(document, documentObjects);

            this.document = document;
            this.DocumentObjects = documentObjects;

            this.document.FormatChanged += (sender, newFormatEventHandler) =>
                PropertyChanged.Raise(this, "Format");
        }

        /// <summary>
        /// Get the format of the document.
        /// </summary>
        public IFormat Format
        {
            get { return document.Format; }
        }

        /// <summary>
        /// Get the documentobjects in the document.
        /// </summary>
        public ReadOnlyObservableCollection<
            IDocumentObjectUiController> DocumentObjects { get; private set; }

        /// <summary>
        /// Occurs when a property on the viewmodel changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
