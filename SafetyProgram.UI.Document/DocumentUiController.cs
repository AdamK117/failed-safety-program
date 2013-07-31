using System.Collections.ObjectModel;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core;
using SafetyProgram.Core.Commands;
using SafetyProgram.Core.Models;
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
            documentRibbonTabs = new ObservableCollection<RibbonTabItem>();
            documentObjects = new ObservableCollection<IDocumentObjectUiController>();

            view = new DocumentView(
                new DocumentViewModel(
                    document,
                    new DocumentICommands(document, commandInvoker)
                )
            );
        }

        private readonly Control view;

        public System.Windows.Controls.Control View
        {
            get { return view; }
        }

        private readonly ObservableCollection<RibbonTabItem> documentRibbonTabs;

        public System.Collections.ObjectModel.ObservableCollection<Fluent.RibbonTabItem> DocumentRibbonTabs
        {
            get { return documentRibbonTabs; }
        }

        private readonly ObservableCollection<IDocumentObjectUiController> documentObjects;

        public System.Collections.ObjectModel.ObservableCollection<DocumentObject.IDocumentObjectUiController> DocumentObjects
        {
            get { return documentObjects; }
        }
    }
}
