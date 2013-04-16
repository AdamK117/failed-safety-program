using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Controls;

using SafetyProgram.BaseClasses;
using SafetyProgram.DocObjects;
using SafetyProgram.Document.Commands;
using SafetyProgram.Document.ContextMenus;

namespace SafetyProgram.Document
{
    public class CoshhDocument : BaseINPC
    {
        private readonly DocumentCommandsHolder commands;
        private readonly DocumentContextMenu contextMenu;
        private readonly CoshhDocumentView view;
        private readonly ObservableCollection<DocObject> body;

        private string title;
        private DocFormat format;        

        private DocObject selected;
        private bool edited;

        /// <summary>
        /// Constructs a new CoshhDocument.
        /// </summary>
        public CoshhDocument()
        {
            body = new ObservableCollection<DocObject>();

            //Monitor changes in the CoshhDocument's body (for selections, ribbons, etc.)
            body.CollectionChanged += bodyChanged;

            title = "Untitled Document";
            format = new DocFormat("630", "891");
            selected = null;
            edited = false;

            commands = new DocumentCommandsHolder(this);
            contextMenu = new DocumentContextMenu(commands);

            view = new CoshhDocumentView(this);
            view.InputBindings.AddRange(commands.Hotkeys);
        }

        /// <summary>
        /// Gets the CoshhDocument's view
        /// </summary>
        public UserControl View
        {
            get { return view; }
        }

        /// <summary>
        /// Gets the general ContextMenu for the CoshhDocument
        /// </summary>
        public DocumentContextMenu ContextMenu
        {
            get 
            { 
                return contextMenu; 
            }
        }

        /// <summary>
        /// Gets the body (content) of the CoshhDocument
        /// </summary>
        public ObservableCollection<DocObject> Body
        {
            get 
            { 
                return body; 
            }
        }

        /// <summary>
        /// Gets the ICommands (and hotkeys associated with) for the CoshhDocument.
        /// </summary>
        public DocumentCommandsHolder Commands
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
        public event titleChangedDelegate TitleChanged;
        /// <summary>
        /// Delegate for TitleChanged
        /// </summary>
        /// <param name="title">The new Title of the CoshhDocument</param>
        public delegate void titleChangedDelegate(string title);

        /// <summary>
        /// Gets/Sets the format (A4 etc.) of the CoshhDocument.
        /// </summary>
        public DocFormat Format
        {
            get 
            {
                return format; 
            }
            set
            {
                format = value;

                if (FormatChanged != null)
                {
                    FormatChanged(format);
                }
                RaisePropertyChanged("Format");
            }
        }
        /// <summary>
        /// Event that triggers if the Format of the CoshhDocument changes.
        /// </summary>
        public event docFormatChangedDelegate FormatChanged;
        /// <summary>
        /// Delegate for FormatChanged.
        /// </summary>
        /// <param name="format">The new CoshhDocument format.</param>
        public delegate void docFormatChangedDelegate(DocFormat format);

        /// <summary>
        /// Selects a DocObject within the CoshhDocument
        /// </summary>
        /// <param name="docObject">The DocObject to be selected.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if a null selection is attempted. Use ClearSelection() instead.</exception>
        public void Select(DocObject docObject)
        {
            //If the selection isn't null (i.e. attempting to select nothing)
            if (docObject != null)
            {
                if (selected != docObject)
                {
                    if (selected != null) 
                    {
                        selected.DeSelect();
                    }

                    selected = docObject;

                    if (SelectionChanged != null)
                    {
                        SelectionChanged(selected);
                    }
                    RaisePropertyChanged("Selected");
                }
            }
            else throw new ArgumentNullException("Attempted to select nothing, use ClearSelection instead when attempting to clear a CoshhDocuments selection");            
        }
        /// <summary>
        /// Clears the current selection of the CoshhDocument.
        /// </summary>
        public void ClearSelection()
        {
            selected = null;

            if (SelectionChanged != null)
            {
                SelectionChanged(null);
            }
            RaisePropertyChanged("Selected");
        }
        /// <summary>
        /// Gets the currently selected DocObject in the CoshhDocument.
        /// </summary>
        public DocObject Selected
        {
            get
            { 
                return selected; 
            }
        }
        /// <summary>
        /// Event that fires if the Selection of the CoshhDocument changes.
        /// </summary>
        public event selectionChangedDelegate SelectionChanged;
        /// <summary>
        /// Delegate for SelectionChanged
        /// </summary>
        /// <param name="selection">The selection of the CoshhDocument.</param>
        public delegate void selectionChangedDelegate(DocObject selection);

        /// <summary>
        /// Marks the CoshhDocument as edited
        /// </summary>
        public void Edited()
        {
            if (edited != true)
            {
                edited = true;

                if (EditedFlagChanged != null)
                {
                    EditedFlagChanged(true);
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
        public event editedFlagDelegate EditedFlagChanged;
        /// <summary>
        /// Delegate for EditedChanged.
        /// </summary>
        /// <param name="edited">The edited state of the CoshhDocument (true = document has been edited)</param>
        public delegate void editedFlagDelegate(bool edited);

        /// <summary>
        /// Handler that monitors when the body changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bodyChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                #region Add
                //If new DocObject(s) were added to the CoshhDocument.
                case NotifyCollectionChangedAction.Add:
                    foreach (DocObject control in e.NewItems)
                    {
                        //Monitor the new DocObject's Remove flag
                        control.RemoveFlagChanged += (DocObject docObject, bool flag) =>
                            {
                                if (flag == true)
                                {
                                    ClearSelection();
                                    body.Remove(docObject);
                                }
                            };
                        //Monitor the DocObjet's Selected flag.
                        control.SelectedChanged += (DocObject docObject, bool flag) =>
                            {
                                if (flag == true)
                                {
                                    Select(docObject);
                                }
                            };
                    }
                    break;
                #endregion

                #region Remove
                //If DocObject(s) are removed from the CoshhDocument.
                case NotifyCollectionChangedAction.Remove:
                    foreach (DocObject control in e.OldItems)
                    {
                        if (Selected == control)
                        {
                            ClearSelection();
                        }
                    }
                    break;
                #endregion
            }
            Edited();
        }
    }
}
