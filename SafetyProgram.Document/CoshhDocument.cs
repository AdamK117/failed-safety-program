using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Controls;

using SafetyProgram.BaseClasses;
using SafetyProgram.BaseClasses.DocumentFormats;
using SafetyProgram.DocObjects;
using SafetyProgram.Document.Commands;
using SafetyProgram.Document.ContextMenus;

namespace SafetyProgram.Document
{
    public class CoshhDocument : BaseINPC, ICoshhDocument
    {
        private readonly DocumentCommandsHolder commands;
        private readonly DocumentContextMenu contextMenu;
        private readonly CoshhDocumentView view;
        private readonly ObservableCollection<IDocObject> body;

        private string title;
        private IDocFormat format;        

        private IDocObject selected;
        private bool edited;

        /// <summary>
        /// Constructs a new CoshhDocument.
        /// </summary>
        public CoshhDocument(IDocFormat format)
        {
            this.format = format;

            body = new ObservableCollection<IDocObject>();
            body.CollectionChanged += bodyChanged;

            title = "Untitled Document";
            
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
        public ObservableCollection<IDocObject> Body
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

        /// <summary>
        /// Selects a DocObject within the CoshhDocument
        /// </summary>
        /// <param name="docObject">The DocObject to be selected.</param>
        /// <exception cref="System.ArgumentNullException">Thrown if a null selection is attempted. Use ClearSelection() instead.</exception>
        public void Select(IDocObject docObject)
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
        public IDocObject Selected
        {
            get
            { 
                return selected; 
            }
        }
        /// <summary>
        /// Event that fires if the Selection of the CoshhDocument changes.
        /// </summary>
        public event Action<IDocObject> SelectionChanged;

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
        public event Action<bool> EditedFlagChanged;

        /// <summary>
        /// Handler that monitors when the body changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bodyChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            
            Edited();
        }
    }
}
