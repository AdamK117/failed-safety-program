using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Commands;

namespace SafetyProgram.Document
{
    public sealed class CoshhDocument : ICoshhDocument
    {
        /// <summary>
        /// Constructs a new CoshhDocument.
        /// </summary>
        public CoshhDocument (
            IConfiguration appConfiguration, 
            string title, 
            IFormat format, 
            IDocumentBody body,
            Func<ICoshhDocument, IDocumentICommands> commandsConstructor,
            Func<IDocumentICommands, IContextMenu> contextMenuConstructor,
            Func<ICoshhDocument, ObservableCollection<IRibbonTabItem>> ribbonTabsConstructor,
            Func<ICoshhDocument, Control> viewConstructor
            )
        {
            editedFlag = false;

            if (
                appConfiguration != null &&
                format != null &&
                title != null &&
                body != null &&
                commandsConstructor != null &&
                contextMenuConstructor != null &&
                ribbonTabsConstructor != null &&
                viewConstructor != null
                )
            {
                this.appConfiguration = appConfiguration;
                this.title = title;
                this.format = format;                

                this.documentBody = body;
                this.documentBody.Items.CollectionChanged += (sender, e) => FlagAsEdited();
                this.documentBody.SelectionChanged += (object sender, GenericPropertyChangedEventArg<IDocumentObject> selection) => documentSelectionChanged(selection.NewProperty);

                documentCommands = commandsConstructor(this);
                contextMenu = contextMenuConstructor(this.Commands);
                ribbonTabItems = ribbonTabsConstructor(this);
                view = viewConstructor(this);
            }
            else throw new ArgumentNullException();
        }

        private readonly IConfiguration appConfiguration;
        public IConfiguration AppConfiguration
        {
            get
            {
                return appConfiguration;
            }
        }

        private readonly Control view;
        /// <summary>
        /// Gets the CoshhDocument's view
        /// </summary>
        public Control View
        {
            get 
            { 
                return view; 
            }
        }

        private readonly IDocumentBody documentBody;
        /// <summary>
        /// Gets the body (content) of the CoshhDocument
        /// </summary>
        public IDocumentBody Body
        {
            get 
            { 
                return documentBody; 
            }
        }

        private readonly IDocumentICommands documentCommands;
        /// <summary>
        /// Gets the ICommands (and hotkeys associated with) for the CoshhDocument.
        /// </summary>
        public IDocumentICommands Commands
        {
            get 
            { 
                return documentCommands; 
            }
        }

        private string title;
        /// <summary>
        /// Gets/Sets the Title of the CoshhDocument.
        /// </summary>
        public string Title
        {
            get
            { 
                return title; 
            }
            set 
            { 
                title = value;
                TitleChanged.Raise(this, title);
                PropertyChanged.Raise(this, "Title");
            }
        }
        /// <summary>
        /// Event that fires if the Title of the CoshhDocument changes.
        /// </summary>
        public event EventHandler<GenericPropertyChangedEventArg<string>> TitleChanged;

        private IFormat format;
        /// <summary>
        /// Gets/Sets the format (A4 etc.) of the CoshhDocument.
        /// </summary>
        public IFormat Format
        {
            get 
            {
                return format; 
            }
        }
        /// <summary>
        /// Change the format of the CoshhDocument
        /// </summary>
        /// <param name="newFormat">The new format</param>
        public void ChangeFormat(IFormat newFormat)
        {
            if (newFormat != null)
            {
                format = newFormat;
                FormatChanged.Raise(this, format);
                PropertyChanged.Raise(this, "Format");
            }
            else throw new ArgumentNullException("The IDocumentFormat supplied must not be null (A document must have a format)");
        }
        /// <summary>
        /// Event that triggers if the Format of the CoshhDocument changes.
        /// </summary>
        public event EventHandler<GenericPropertyChangedEventArg<IFormat>> FormatChanged;

        private readonly IContextMenu contextMenu;
        /// <summary>
        /// Gets the general ContextMenu for the CoshhDocument
        /// </summary>
        public IContextMenu ContextMenu
        {
            get
            {
                return contextMenu;
            }
        }

        private bool removeFlag;
        /// <summary>
        /// Flag the CoshhDocument for removal
        /// </summary>
        public void FlagForRemoval()
        {
            if (removeFlag != true)
            {
                removeFlag = true;
                RemoveFlagChanged.Raise(this, removeFlag);
                PropertyChanged.Raise(this, "RemoveFlag");
            }
        }
        public bool RemoveFlag
        {
            get 
            {
                return removeFlag; 
            }
        }
        public event EventHandler<GenericPropertyChangedEventArg<bool>> RemoveFlagChanged;

        private bool editedFlag;
        /// <summary>
        /// Marks the CoshhDocument as edited
        /// </summary>
        public void FlagAsEdited()
        {
            if (editedFlag != true)
            {
                editedFlag = true;
                EditedFlagChanged.Raise(this, editedFlag);
                PropertyChanged.Raise(this, "EditedFlag");
            }
        }
        /// <summary>
        /// Gets a flag that indicates if the CoshhDocument has been edited
        /// </summary>
        public bool EditedFlag
        {
            get
            {
                return editedFlag;
            }
        }
        /// <summary>
        /// Event that fires if the edited state of the CoshhDocument changes.
        /// </summary>
        public event EventHandler<GenericPropertyChangedEventArg<bool>> EditedFlagChanged;

        private IList<string> validationErrorList = new List<string>();

        public string Error
        {
            get 
            {
                return ErrorValidation.JoinErrors(validationErrorList);
            }
        }

        public string this[string columnName]
        {
            get 
            {
                validationErrorList.Clear();

                //No validation checks for the document (yet)

                return null;
            }
        }

        private readonly ObservableCollection<IRibbonTabItem> ribbonTabItems;
        public ObservableCollection<IRibbonTabItem> RibbonTabs
        {
            get 
            { 
                return ribbonTabItems; 
            }
        }

        private ISelectable currentSelection;
        private void documentSelectionChanged(IDocumentObject newSelection)
        {
            //3 Scenarios: 
            //  The same ISelectable was re-selected: Do nothing
            //  The selection is cleared (null): Ribbon needs to clear out redundant tabs
            //  The selection is different: Remove any old tabs, add a new tab, focus it.

            //The Selection is cleared
            if (newSelection == null)
            {
                if (currentSelection != null)
                {
                    RibbonTabs.Remove(currentSelection.RibbonTab);
                }                
                currentSelection = null;
            }
            //The Selection is different
            else if (newSelection != currentSelection)
            {
                if (currentSelection != null)
                {
                    RibbonTabs.Remove(currentSelection.RibbonTab);
                    currentSelection = null;
                }                
                currentSelection = newSelection;

                RibbonTabs.Add(newSelection.RibbonTab);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
