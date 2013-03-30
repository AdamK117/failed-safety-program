using System.Collections.ObjectModel;
using System.Windows.Controls;
using SafetyProgram.MainWindow.Document.Controls;

namespace SafetyProgram.MainWindow.Document
{
    public class CoshhDocument : BaseINPC
    {
        protected ObservableCollection<IDocObject> body;
        protected string title;
        protected IDocObject selected;
        protected bool edited;
        protected readonly CoshhDocumentView view;

        /// <summary>
        /// Constructs a new CoshhDocument.
        /// </summary>
        public CoshhDocument() 
        {
            body = new ObservableCollection<IDocObject>();
            title = "Untitled Document";
            selected = null;
            edited = false;

            view = new CoshhDocumentView(this);
        }

        /// <summary>
        /// Gets the document UserControl
        /// </summary>
        /// <returns>CoshhDocument View</returns>
        public UserControl View
        {
            get { return view; }
        }

        /// <summary>
        /// Gets/Sets the body of the document.
        /// </summary>
        public ObservableCollection<IDocObject> Body
        {
            get { return body; }
            set 
            { 
                body = value;
                RaisePropertyChanged("Body");
            }
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

        public IDocObject Selected
        {
            get
            {
                return selected;
            }
            set
            {
                if (this.selected != value)
                {
                    this.selected = value;
                    if (SelectionChanged != null) { SelectionChanged(value); }
                    RaisePropertyChanged("Selected");
                }
            }
        }
        
        /// <summary>
        /// Event that fires if a new selection is made within the document.
        /// </summary>
        public event selectionChangedDelegate SelectionChanged;
        public delegate void selectionChangedDelegate(IDocObject selection);

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
        
        /// <summary>
        /// Event that fires when the document is edited.
        /// </summary>
        public event editedDelegate EditedChanged;
        public delegate void editedDelegate(bool edited);
    }
}
