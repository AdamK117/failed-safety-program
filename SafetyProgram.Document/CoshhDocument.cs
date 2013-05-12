﻿using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Commands;
using SafetyProgram.Document.ContextMenus;
using SafetyProgram.Document.Ribbons;

namespace SafetyProgram.Document
{
    public sealed class CoshhDocument : INotifyPropertyChanged, IDocument
    {
        /// <summary>
        /// Constructs a new CoshhDocument.
        /// </summary>
        public CoshhDocument (
            IConfiguration appConfiguration, 
            string title, 
            IDocFormat format, 
            IDocumentBody body,
            Func<CoshhDocument, Control> viewConstructor
            )
        {
            if (appConfiguration != null) this.appConfiguration = appConfiguration;
            else throw new ArgumentNullException();

            if (format != null) this.format = format;
            else throw new ArgumentNullException();

            if (title != null) this.title = title;
            else throw new ArgumentNullException();

            if (body != null)
            {
                this.body = body;
                this.body.Items.CollectionChanged += (sender, e) => FlagAsEdited();
                this.body.SelectionChanged += (IDocumentObject selection) => documentSelectionChanged(selection); 
            }
            else throw new ArgumentNullException();           
           
            edited = false;

            commands = new DocumentICommands(this);
            contextMenu = new DocumentContextMenu(commands);

            //Insert tab
            ribbonTabItems = new ObservableCollection<IRibbonTabItem>();            
            ribbonTabItems.Add(new CoshhDocumentRibbonTab(this));

            if (viewConstructor != null) view = viewConstructor(this);
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

        private readonly IDocumentBody body;
        /// <summary>
        /// Gets the body (content) of the CoshhDocument
        /// </summary>
        public IDocumentBody Body
        {
            get 
            { 
                return body; 
            }
        }

        private readonly IDocumentICommands commands;
        /// <summary>
        /// Gets the ICommands (and hotkeys associated with) for the CoshhDocument.
        /// </summary>
        public IDocumentICommands Commands
        {
            get 
            { 
                return commands; 
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

                if (TitleChanged != null)
                {
                    TitleChanged(title);
                }
                PropertyChanged.Raise(this, "Title");
            }
        }
        /// <summary>
        /// Event that fires if the Title of the CoshhDocument changes.
        /// </summary>
        public event Action<string> TitleChanged;

        private IDocFormat format;
        /// <summary>
        /// Gets/Sets the format (A4 etc.) of the CoshhDocument.
        /// </summary>
        public IDocFormat Format
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
        public void ChangeFormat(IDocFormat newFormat)
        {
            if (newFormat != null)
            {
                format = newFormat;

                if (FormatChanged != null)
                {
                    FormatChanged(format);
                }
                PropertyChanged.Raise(this, "Format");
            }
            else throw new ArgumentNullException("The IDocumentFormat supplied must not be null (A document must have a format)");
        }
        /// <summary>
        /// Event that triggers if the Format of the CoshhDocument changes.
        /// </summary>
        public event Action<IDocFormat> FormatChanged;

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
                if (RemoveFlagChanged != null)
                {
                    RemoveFlagChanged(this, removeFlag);
                }
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
        public event Action<object, bool> RemoveFlagChanged;

        private bool edited;
        /// <summary>
        /// Marks the CoshhDocument as edited
        /// </summary>
        public void FlagAsEdited()
        {
            if (edited != true)
            {
                edited = true;

                if (EditedFlagChanged != null)
                {
                    EditedFlagChanged(this, true);
                }
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
                return edited;
            }
        }
        /// <summary>
        /// Event that fires if the edited state of the CoshhDocument changes.
        /// </summary>
        public event Action<object, bool> EditedFlagChanged;

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        private readonly ObservableCollection<IRibbonTabItem> ribbonTabItems;
        public ObservableCollection<IRibbonTabItem> RibbonTabs
        {
            get 
            { 
                return ribbonTabItems; 
            }
        }

        private ISelectable oldSelection;
        private void documentSelectionChanged(IDocumentObject newSelection)
        {
            //3 Scenarios: 
            //  The same ISelectable was re-selected: Do nothing
            //  The selection is cleared (null): Ribbon needs to clear out redundant tabs
            //  The selection is different: Remove any old tabs, add a new tab, focus it.

            //The Selection is cleared
            if (newSelection == null)
            {
                if (oldSelection != null)
                {
                    RibbonTabs.Remove(oldSelection.RibbonTab);
                }                
                oldSelection = null;
            }
            //The Selection is different
            else if (newSelection != oldSelection)
            {
                if (oldSelection != null)
                {
                    RibbonTabs.Remove(oldSelection.RibbonTab);
                    oldSelection = null;
                }                
                oldSelection = newSelection;

                RibbonTabs.Add(newSelection.RibbonTab);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
