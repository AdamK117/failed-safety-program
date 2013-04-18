using System.Windows.Controls;
using System.Xml.Linq;
using SafetyProgram.BaseClasses;
using System;

namespace SafetyProgram.DocObjects
{
    public abstract class DocObject : BaseINPC, IDocObject
    {
        /// <summary>
        /// Gets the UserControl view for the DocObject.
        /// </summary>
        public abstract UserControl View { get; }

        /// <summary>
        /// Gets the contextual ribbon for the DocObject.
        /// </summary>
        public abstract IRibbonTabItem RibbonTab { get; }

        /// <summary>
        /// Gets the context menu for the DocObject.
        /// </summary>
        public abstract IContextMenu ContextMenu { get; }

        #region Selection Logic
        
        private bool selectedFlag;

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
        /// Event that fires if the DocObject Selected state has changed.
        /// </summary>
        public event Action<IDocObject, bool> SelectedChanged;

        #endregion

        #region Edited Logic

        private bool editedFlag;

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
        /// Event that fires if the DocObjects EditedFlag has changed.
        /// </summary>
        public event Action<IDocObject, bool> EditedChanged;

        #endregion

        #region Removal Logic

        private bool removeFlag;

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
        /// Event that fires if the state of RemoveFlag changes.
        /// </summary>
        public event Action<IDocObject, bool> RemoveFlagChanged;

        #endregion

        #region Input/Output (IO) logic

        /// <summary>
        /// Loads Xml (as an XElement) data into the DocObject
        /// </summary>
        /// <param name="data">XElement representation of the DocObject</param>
        public abstract void Load(XElement data);

        /// <summary>
        /// Saves the DocObject to an XElement.
        /// </summary>
        /// <returns>XElement containing the DocObjects data.</returns>
        public abstract XElement Save();

        #endregion
    }
}
