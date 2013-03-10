using SafetyProgram.UserControls;
using System.Collections.ObjectModel;
using System;
using System.Windows.Forms;

namespace SafetyProgram.Data.DOM
{
    public class CoshhDocument
    {
        protected CoshhWindow parent;
        protected ObservableCollection<IDocObject> body;

        public CoshhDocument(CoshhWindow parent)
        {
            this.parent = parent;
        }

        /// <summary>
        /// Returns the body (contents) of the document.
        /// </summary>
        public ObservableCollection<IDocObject> Body
        {
            get { return body; }
            set { body = value; }
        }

        protected string title = "Untitled Document";
        /// <summary>
        /// Returns the Title of the document.
        /// </summary>
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        #region Selection Logic

        private IDocObject selected;
        /// <summary>
        /// Set the document object as selected.
        /// </summary>
        /// <param name="selected">The selected document object</param>
        /// <returns></returns>
        public IDocObject Selected(IDocObject selected)
        {
            if (this.selected != selected)
            {
                this.selected = selected;
                if (SelectionChanged != null) { SelectionChanged(selected); }
            }
            return selected;
        }
        /// <summary>
        /// Returns the currently selected (if any) object.
        /// </summary>
        /// <returns></returns>
        public IDocObject Selected() { return selected; }
        public delegate void selectionChangedDelegate(IDocObject selection);
        /// <summary>
        /// Event that fires if a new selection is made within the document.
        /// </summary>
        public event selectionChangedDelegate SelectionChanged;

        #endregion

        #region IsOpen logic

        private bool isOpen;
        /// <summary>
        /// Sets if the document is open (displayed) or not.
        /// </summary>
        /// <param name="isOpen"></param>
        /// <returns></returns>
        public bool IsOpen(bool isOpen)
        {
            if (this.isOpen != isOpen)
            {
                this.isOpen = isOpen;
                if (IsOpenChanged != null) { IsOpenChanged(isOpen); }
            }
            return isOpen;
        }
        /// <summary>
        /// Returns the open state of the document
        /// </summary>
        /// <returns></returns>
        public bool IsOpen() { return isOpen; }
        public delegate void isOpenChangedDelegate(bool isOpen);
        /// <summary>
        /// Event that fires if the open state (isOpen) of the document changes
        /// </summary>
        public event isOpenChangedDelegate IsOpenChanged;

        #endregion

        #region (Document) Edited logic

        private bool edited;
        /// <summary>
        /// Sets if the file has been edited or not.
        /// </summary>
        /// <param name="edited"></param>
        /// <returns></returns>
        public bool Edited(bool edited)
        {
            if (this.edited != edited)
            {
                this.edited = edited;
                if (EditedChanged != null) { EditedChanged(edited); }
            }
            return edited;
        }
        /// <summary>
        /// Returns if the document has been edited.
        /// </summary>
        /// <returns></returns>
        public bool Edited() { return edited; }
        public delegate void editedDelegate(bool edited);
        /// <summary>
        /// Event that fires when the document is edited.
        /// </summary>
        public event editedDelegate EditedChanged;

        #endregion
    }
}
