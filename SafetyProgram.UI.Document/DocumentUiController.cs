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
        public DocumentUiController(IDocument document, 
            IConfiguration configuration, 
            ICommandInvoker commandInvoker)
        {
            IDocumentObjectUiControllerFactory fac = null;

            // Hold the document contents.
            documentObjects = new ObservableCollection<IDocumentObjectUiController>();

            foreach (IDocumentObject documentObject in document.Items)
            {
                var docUiController = fac.GetDocumentObjectUiController(documentObject);
                documentObjects.Add(docUiController);
            }

            // Construct commands to work on the document.
            var documentCommands = new DocumentICommands(document, commandInvoker);

            // Construct the document view.
            view = new DocumentView(
                new DocumentViewModel(
                    documentObjects,
                    documentCommands
                )
            );

            // Construct the ribbon tabs exposed by the document.
            documentRibbonTabs = new ObservableCollection<RibbonTabItem>();

            var insertTab = new InsertRibbonTabView(
                new InsertRibbonTabViewModel(
                    documentCommands
                )
            );

            documentRibbonTabs.Add(insertTab);
        }

        private readonly Control view;

        /// <summary>
        /// Get the view for the document.
        /// </summary>
        public Control View
        {
            get { return view; }
        }

        private readonly ObservableCollection<RibbonTabItem> documentRibbonTabs;

        /// <summary>
        /// Get the ribbon tab items associated with the document (insert, change layout, etc.).
        /// </summary>
        public ObservableCollection<RibbonTabItem> DocumentRibbonTabs
        {
            get { return documentRibbonTabs; }
        }

        private readonly ObservableCollection<IDocumentObjectUiController> documentObjects;

        /// <summary>
        /// Get the document objects in the document.
        /// </summary>
        public ObservableCollection<IDocumentObjectUiController> DocumentObjects
        {
            get { return documentObjects; }
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
        public event EventHandler<GenericPropertyChangedEventArg<IDocumentObjectUiController>> SelectionChanged;
    }
}
