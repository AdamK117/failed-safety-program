using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Controls;

using SafetyProgram.MainWindow.Document.Commands;
using SafetyProgram.MainWindow.Document.ContextMenus;
using SafetyProgram.MainWindow.Document.Controls;

namespace SafetyProgram.MainWindow.Document
{
    public class CoshhDocument : BaseINPC
    {
        private readonly DocumentCommandsHolder commands;
        private readonly DocumentContextMenu contextMenu;
        private readonly CoshhDocumentView view;       

        private string title;
        private DocFormat format;
        private readonly ObservableCollection<DocObject> body;
        
        private DocObject selected;
        private bool edited;

        /// <summary>
        /// Constructs a new CoshhDocument.
        /// </summary>
        public CoshhDocument()
        {
            body = new ObservableCollection<DocObject>();
            body.CollectionChanged += bodyChanged;

            title = "Untitled Document";
            format = new DocFormat("630", "891");

            selected = null;
            edited = false;
            contextMenu = new DocumentContextMenu(this);
            commands = new DocumentCommandsHolder(this);

            view = new CoshhDocumentView(this);
            view.InputBindings.AddRange(commands.Hotkeys);
        }

        /// <summary>
        /// Gets the document UserControl
        /// </summary>
        public UserControl View
        {
            get { return view; }
        }

        /// <summary>
        /// Gets the context menu for the document (general use context menu, not control dependant).
        /// </summary>
        public DocumentContextMenu ContextMenu
        {
            get { return contextMenu; }
        }

        /// <summary>
        /// Gets/Sets the body of the document.
        /// </summary>
        public ObservableCollection<DocObject> Body
        {
            get { return body; }
        }

        /// <summary>
        /// Gets/Sets the Title of the document.
        /// </summary>
        public string Title
        {
            get { return title; }
            set 
            { 
                title = value;
                RaisePropertyChanged("Title");
            }
        }

        /// <summary>
        /// Gets/Sets the format of the document (A4, etc.)
        /// </summary>
        public DocFormat Format
        {
            get { return format; }
            set
            {
                format = value;
                RaisePropertyChanged("Format");
            }
        }

        /// <summary>
        /// Gets the ICommands (and hotkeys associated with) for the document.
        /// </summary>
        public DocumentCommandsHolder Commands
        {
            get { return commands; }
        }

        /// <summary>
        /// Gets/Sets the currently selected DocObject (table, textbox, etc.) inthe document.
        /// </summary>
        public DocObject Selected
        {
            get{ return selected; }
            set
            {
                //If the selection is different
                if (selected != value)
                {
                    //If there was an old selection, deselect it.
                    if (selected != null) { selected.Selected = false; }

                    //Set the new selection
                    selected = value;

                    //Trigger event handlers
                    if (SelectionChanged != null) { SelectionChanged(selected); }
                    RaisePropertyChanged("Selected");
                }
            }
        }
        public event selectionChangedDelegate SelectionChanged;
        public delegate void selectionChangedDelegate(DocObject selection);

        /// <summary>
        /// Gets/Sets the document edited flag
        /// </summary>
        public bool Edited
        {
            get
            {
                return edited;
            }
            set
            {
                if (value != edited)
                {
                    edited = value;
                    if (EditedChanged != null) { EditedChanged(value); }
                    RaisePropertyChanged("Edited");
                }
            }
        }
        public event editedDelegate EditedChanged;
        public delegate void editedDelegate(bool edited);

        /// <summary>
        /// Handler that monitors when the body changes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bodyChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                //If a new DocObject was added to the body
                case NotifyCollectionChangedAction.Add:
                    foreach (DocObject control in e.NewItems)
                    {
                        //Handler if it was flagged for removal.
                        control.RemoveFlagChanged += (DocObject docObject, bool flag) =>
                            {
                                if (flag == true)
                                {
                                    Selected = null;
                                    body.Remove(docObject);
                                }
                            };
                    }
                    break;
            }
        }
    }
}
