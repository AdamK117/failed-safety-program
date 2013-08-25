using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands;
using SafetyProgram.Core.Models;
using SafetyProgram.Document.Ribbons;
using SafetyProgram.UI.Document.View;
using SafetyProgram.UI.DocumentObject;

namespace SafetyProgram.UI.Document
{
    public sealed class DocumentUiController : IDocumentUiController
    {
        private readonly IDocumentObjectUiControllerFactory documentObjectControllerFactory;

        public DocumentUiController(IDocument document, 
            IConfiguration configuration, 
            ICommandInvoker commandInvoker)
        {
            documentObjectControllerFactory = new DocumentObjectUiControllerFactory(
                configuration,
                commandInvoker);

            documentObjectControllers = new LinkedReadOnlyObservableCollection
                <IDocumentObject, IDocumentObjectUiController>(
                document.Items,
                documentObjectControllerFactory.GetDocumentObjectUiController);

            // Construct commands to work on the document.
            var documentCommands = new DocumentICommands(document, commandInvoker);

            // Construct the document view.
            view = new DocumentView(
                new DocumentViewModel(
                    document,
                    documentObjectControllers,
                    documentCommands));

            documentRibbonTabs.Add(
                new InsertRibbonTabView(
                    new InsertRibbonTabViewModel(
                        documentCommands)));
        }

        private readonly Control view;

        /// <summary>
        /// Get the view for the document.
        /// </summary>
        public Control View
        {
            get { return view; }
        }

        private readonly ObservableCollection<RibbonTabItem> documentRibbonTabs 
            = new ObservableCollection<RibbonTabItem>();

        /// <summary>
        /// Get the ribbon tab items associated with the document (insert, change layout, etc.).
        /// </summary>
        public ObservableCollection<RibbonTabItem> DocumentRibbonTabs
        {
            get { return documentRibbonTabs; }
        }

        private readonly ReadOnlyObservableCollection<IDocumentObjectUiController> documentObjectControllers;

        /// <summary>
        /// Get the document objects in the document.
        /// </summary>
        public ReadOnlyObservableCollection<IDocumentObjectUiController> DocumentObjectControllers
        {
            get { return documentObjectControllers; }
        }

        IDocumentObjectUiController selection;

        /// <summary>
        /// Get the selected documentobject in the document (nullable).
        /// </summary>
        public IDocumentObjectUiController Selection
        {
            get { return selection; }
            set 
            { 
                selection = value;
                SelectionChanged.Raise(this, selection);
            }
        }

        /// <summary>
        /// Occurs when the selection changes.
        /// </summary>
        public event EventHandler<
            GenericPropertyChangedEventArg<IDocumentObjectUiController>> SelectionChanged;
    }
}
