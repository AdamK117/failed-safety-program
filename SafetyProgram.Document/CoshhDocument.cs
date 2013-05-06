﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Controls;
using System.Xml.Linq;

using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects;
using SafetyProgram.Document.Body;
using SafetyProgram.Document.Commands;
using SafetyProgram.Document.ContextMenus;
using SafetyProgram.Document.Ribbons;
using SafetyProgram.Static;
using SafetyProgram.Base.DocumentFormats;

namespace SafetyProgram.Document
{
    public sealed class CoshhDocument : BaseINPC, IDocument
    {
        private readonly DocumentICommands commands;
        private readonly IContextMenu contextMenu;
        private readonly CoshhDocumentView view;
        private readonly ObservableCollection<IRibbonTabItem> ribbonTabItems;

        private readonly IDocumentBody body;
        private string title;
        private IDocFormat format;      

        private bool edited;

        /// <summary>
        /// Constructs a new CoshhDocument.
        /// </summary>
        public CoshhDocument(string title, IDocFormat format, IDocumentBody body)
        {
            this.format = format;
            this.title = title;
            this.body = body;

            this.body.Items.CollectionChanged += (sender, e) => FlagAsEdited();
            this.body.SelectionChanged += (IDocumentObject selection) => documentSelectionChanged(selection); 
           
            edited = false;

            commands = new DocumentICommands(this);
            contextMenu = new DocumentContextMenu(commands);

            ribbonTabItems = new ObservableCollection<IRibbonTabItem>();

            //Insert tab
            ribbonTabItems.Add(new CoshhDocumentRibbonTab(this));

            view = new CoshhDocumentView(this);
            view.InputBindings.AddRange(commands.Hotkeys);
        }

        /// <summary>
        /// Gets the CoshhDocument's view
        /// </summary>
        public Control View
        {
            get { return view; }
        }

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

        /// <summary>
        /// Gets the ICommands (and hotkeys associated with) for the CoshhDocument.
        /// </summary>
        public DocumentICommands Commands
        {
            get 
            { 
                return commands; 
            }
        }

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
                RaisePropertyChanged("Title");
            }
        }
        /// <summary>
        /// Event that fires if the Title of the CoshhDocument changes.
        /// </summary>
        public event Action<string> TitleChanged;

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
            format = newFormat;

            if (FormatChanged != null)
            {
                FormatChanged(format);
            }
            RaisePropertyChanged("Format");
        }
        /// <summary>
        /// Event that triggers if the Format of the CoshhDocument changes.
        /// </summary>
        public event Action<IDocFormat> FormatChanged;

        #region IStorable (Input/Output) Implementation

        /// <summary>
        /// Loads data (Xml format) into the CoshhDocument
        /// </summary>
        /// <param name="data">The data to be loaded into the CoshhDocument</param>
        public IDocument LoadFromXml(XElement data)
        {
            string loadedTitle;
            IDocFormat loadedFormat;
            IDocumentBody loadedBody;

            if (data != null)
            {
                if (data.Attribute("title") != null)
                {
                    loadedTitle = data.Attribute("title").Value;
                }
                else
                {
                    Debug.Write("WARNING: When loading a CoshhDocument a title could not be found, set to default");
                    loadedTitle = "Untitled CoshhDocument";
                }

                loadedFormat = new A4DocFormat();

                loadedBody = new CoshhDocumentBody(DocObjectRegistry.GetDocObjects(data));
            }
            else throw new InvalidDataException("No CoshhDocument root could be found (<coshh></coshh>)");

            return new CoshhDocument(loadedTitle, loadedFormat, loadedBody);
        }

        /// <summary>
        /// Writes the CoshhDocument to an XElement
        /// </summary>
        /// <returns></returns>
        public XElement WriteToXElement()
        {
            //TODO: Validation checks
            ICollection<XElement> content = new List<XElement>();

            foreach (IDocumentObject docObject in Body.Items)
            {
                content.Add(docObject.WriteToXElement());
            }

            XElement xDoc = new XElement("coshh", content);

            return xDoc;
        }

        public string Identifier { get { return XmlNodeNames.CoshhDocument; } }

        #endregion

        #region IInteractable (ContextMenu, Removable, Editable) implementation

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
                RaisePropertyChanged("RemoveFlag");
            }
        }

        public bool RemoveFlag
        {
            get { return removeFlag; }
        }

        public event Action<object, bool> RemoveFlagChanged;

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
                RaisePropertyChanged("Edited");
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

        #endregion

        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        public string this[string columnName]
        {
            get { throw new NotImplementedException(); }
        }

        public ObservableCollection<IRibbonTabItem> RibbonTabs
        {
            get { return ribbonTabItems; }
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
    }
}
