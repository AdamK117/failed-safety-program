using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.DocumentObject;

namespace SafetyProgram.UI.ModelViews.Documents.Default
{
    /// <summary>
    /// Defines a standard implementation of an IDocumentViewmodel.
    /// </summary>
    internal sealed class DocumentViewModel : IDocumentViewModel
    {
        private readonly IDocument model;

        /// <summary>
        /// Construct an instance of a document viewmodel.
        /// </summary>
        /// <param name="document">The underlying document model.</param>
        /// <param name="documentCommands">Commands that act on the document model.</param>
        public DocumentViewModel(IDocument model,
            IApplicationConfiguration configuration,
            ICommandInvoker commandInvoker,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(model,
                configuration,
                commandInvoker,
                selectionManager);

            this.model = model;

            this.DocumentObjects =
                model
                .Content
                .EchoCollection(
                    (modelObj) =>
                    {
                        var fac = new DocumentObjectUiControllerFactory(
                            configuration,
                            commandInvoker,
                            selectionManager);

                        return fac.GetDocumentObjectUiController(modelObj);
                    });

            model.FormatChanged +=
                (s, e) => this.PropertyChanged.Raise(this, "Format");
        }

        /// <summary>
        /// Get the format of the document.
        /// </summary>
        public IFormat Format
        {
            get { return model.Format; }
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
