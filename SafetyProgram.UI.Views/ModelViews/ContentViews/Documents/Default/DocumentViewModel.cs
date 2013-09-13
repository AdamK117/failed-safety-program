using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.Views.ModelViews.Documents.Default
{
    /// <summary>
    /// Defines a standard implementation of an IDocumentViewmodel.
    /// </summary>
    public sealed class DocumentViewModel : IDocumentViewModel
    {
        /// <summary>
        /// Construct an instance of a document viewmodel.
        /// </summary>
        /// <param name="document">The underlying document model.</param>
        /// <param name="documentCommands">Commands that act on the document model.</param>
        public DocumentViewModel(IDocument model,
            Func<IDocumentObject, Control> documentObjectViewFactory)
        {
            Helpers.NullCheck(model,
                documentObjectViewFactory);

            this.DocumentObjects =
                model
                .Content
                .EchoCollection(documentObjectViewFactory);

            model.FormatChanged +=
                (s, e) => this.Format = e.NewProperty;
            this.format = model.Format;
        }

        private IFormat format;

        /// <summary>
        /// Get the format of the document.
        /// </summary>
        public IFormat Format
        {
            get { return format; }
            set
            {
                format = value;
                PropertyChanged.Raise(this, "Format");
            }
        }

        /// <summary>
        /// Get the documentobjects in the document.
        /// </summary>
        public ReadOnlyObservableCollection<Control> DocumentObjects { get; private set; }

        /// <summary>
        /// Occurs when a property on the viewmodel changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
