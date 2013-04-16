using System.Windows.Controls;
using System.Xml.Linq;
using SafetyProgram.BaseClasses;

namespace SafetyProgram.DocObjects
{
    public abstract class DocObject : BaseINPC
    {
        private bool selectedFlag, removeFlag, editedFlag;

        /// <summary>
        /// Gets the UserControl view for the DocObject.
        /// </summary>
        public abstract UserControl View { get; }

        /// <summary>
        /// Gets the contextual ribbon for the DocObject.
        /// </summary>
        public abstract IDocObjectRibbonTab Ribbon { get; }

        /// <summary>
        /// Gets the context menu for the DocObject.
        /// </summary>
        public abstract IDocObjectContextMenu ContextMenu { get; }

        #region Selection Logic

        /// <summary>
        /// Selects the DocObject.
        /// </summary>
        public virtual void Select()
        {
            //If the flag is actually changing
            if (selectedFlag != true)
            {
                selectedFlag = true;

                if (SelectedChanged != null)
                {
                    SelectedChanged(this, selectedFlag);
                }
            }
        }
        /// <summary>
        /// Deselects the DocObject.
        /// </summary>
        public virtual void DeSelect()
        {
            //If the flag is actually changing
            if (selectedFlag != false)
            {
                selectedFlag = false;

                if (SelectedChanged != null)
                {
                    SelectedChanged(this, selectedFlag);
                }
            }
        }
        /// <summary>
        /// Gets if the DocObject is selected.
        /// </summary>
        public bool Selected
        {
            get { return selectedFlag; }
        }
        /// <summary>
        /// Delegate for SelectionChanged event.
        /// </summary>
        /// <param name="docObject">DocObject that triggered the SelectionChanged event</param>
        /// <param name="selected">If the DocObject is selected.</param>
        public delegate void SelectedChangedDelegate(DocObject docObject, bool selected);
        /// <summary>
        /// Event that fires if the DocObject Selected state has changed.
        /// </summary>
        public event SelectedChangedDelegate SelectedChanged;

        #endregion

        #region Edited Logic

        /// <summary>
        /// Marks the DocObject as edited.
        /// </summary>
        public virtual void Edited()
        {
            if (editedFlag == false)
            {
                editedFlag = true;
                if (EditedChanged != null)
                {
                    EditedChanged(this, editedFlag);
                }
                RaisePropertyChanged("Edited");
            }
        }
        /// <summary>
        /// Gets if the DocObject has been edited.
        /// </summary>
        public bool EditedFlag
        {
            get 
            { 
                return editedFlag; 
            }
        }
        /// <summary>
        /// Delegate for the EditedChanged event.
        /// </summary>
        /// <param name="docObject">DocObject that called EditedChanged.</param>
        /// <param name="edited">Indicates if the DocObject has been edited.</param>
        public delegate void EditedChangedDelegate(DocObject docObject, bool edited);
        /// <summary>
        /// Event that fires if the DocObjects EditedFlag has changed.
        /// </summary>
        public event EditedChangedDelegate EditedChanged;

        #endregion

        #region Removal Logic

        /// <summary>
        /// Flags the DocObject for removal. Will set RemoveFlag to true and call RemoveFlagChanged.
        /// </summary>
        public virtual void FlagForRemoval()
        {
            DeSelect();

            removeFlag = true;
            if (RemoveFlagChanged != null)
            {
                RemoveFlagChanged(this, removeFlag);
            }
            RaisePropertyChanged("RemoveFlag");
        }
        /// <summary>
        /// Gets the current flag for removal of the DocObject.
        /// </summary>
        public bool RemoveFlag
        {
            get 
            {
                return removeFlag; 
            }
        }
        /// <summary>
        /// Delegate for the RemoveFlagChanged event.
        /// </summary>
        /// <param name="docObject">DocObject that has been flagged for removal.</param>
        /// <param name="removalFlag">Removal flag for the DocObject.</param>
        public delegate void removeFlagDelegate(DocObject docObject, bool removalFlag);
        /// <summary>
        /// Event that fires if the state of RemoveFlag changes.
        /// </summary>
        public event removeFlagDelegate RemoveFlagChanged;

        #endregion

        #region Output (IO) logic

        /// <summary>
        /// Saves the DocObject to an XElement.
        /// </summary>
        /// <returns>XElement containing the DocObjects data.</returns>
        public abstract XElement Save();

        #endregion
    }
}
