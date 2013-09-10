using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core.Commands;
using SafetyProgram.UI.Document;

namespace SafetyProgram.UI
{
    public sealed class RibbonViewModel : IRibbonViewModel
    {
        private readonly IHolder<IDocumentController> documentUiControllerHolder;

        public RibbonViewModel(ICoreCommands coreCommands, 
            IHolder<IDocumentController> documentUiControllerHolder)
        {
            this.coreCommands = coreCommands;
            this.documentUiControllerHolder = documentUiControllerHolder;

            documentUiControllerHolder.ContentChanged +=
                (s, newController) =>
                {
                    PropertyChanged.Raise(this, "DocumentRibbonTabs");
                    if (documentUiControllerHolder.Content == null)
                    {
                        DocumentRibbonTabsHolderChanged.Raise(this, null);
                    }
                    else
                    {
                        DocumentRibbonTabsHolderChanged.Raise(this, documentUiControllerHolder.Content.DocumentRibbonTabs);
                    }                    
                    PropertyChanged.Raise(this, "ContextualRibbonTabs");
                };
        }

        /// <summary>
        /// Get the current document ribbon tabs.
        /// </summary>
        public ICollection<RibbonTabItem> DocumentRibbonTabs
        {
            get 
            { 
                return documentUiControllerHolder.Content == null ?
                    null :
                    documentUiControllerHolder.Content.DocumentRibbonTabs;
            }
        }

        /// <summary>
        /// Occurs when the document ribbon tabs changes.
        /// </summary>
        public event EventHandler<
            GenericPropertyChangedEventArg<
                ICollection<RibbonTabItem>>> DocumentRibbonTabsHolderChanged;

        /// <summary>
        /// Get the contextual ribbon tabs assoicated with the currently open document.
        /// </summary>
        public ObservableCollection<RibbonTabItem> ContextualRibbonTabs
        {
            get 
            {
                var selection = documentUiControllerHolder.Content.Selection;

                return selection != null ? 
                    // Get the selection.
                    selection.ContextualTabs : 
                    // Or if null, return empty tabs.
                    new ObservableCollection<RibbonTabItem>();          
            }
        }

        /// <summary>
        /// Occurs when the contextual ribbontabs (the tabs associated with the current 
        /// document) change.
        /// </summary>
        public event EventHandler<
            GenericPropertyChangedEventArg<
                ObservableCollection<RibbonTabItem>>> ContextualRibbonTabsHolderChanged;

        private readonly ICoreCommands coreCommands;

        /// <summary>
        /// Get application commands (new, save, saveas, close, etc.)
        /// </summary>
        public ICoreCommands Commands
        {
            get { return coreCommands; }
        }

        /// <summary>
        /// Occurs when a property changes in this viewmodel.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
