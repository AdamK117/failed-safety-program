using System;
using System.Windows.Controls;
using System.Xml.Linq;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects
{
    public abstract class DocumentObject : BaseINPC, IDocumentObject
    {
        /// <summary>
        /// Gets the UserControl view for the DocObject.
        /// </summary>
        public abstract Control View { get; }

        #region ISelectable: Select, Contextual Ribbon Tab logic
        
        private bool selectedFlag;

        /// <summary>
        /// Gets the contextual ribbon for the DocObject.
        /// </summary>
        public abstract IRibbonTabItem RibbonTab { get; }

        /// <summary>
        /// Selects the DocObject.
        /// </summary>
        public virtual void Select()
        {
            //If the flag is actually changing
            if (selectedFlag == false)
            {
                selectedFlag = true;

                if (SelectedChanged != null)
                {
                    SelectedChanged(this, selectedFlag);
                }
                RaisePropertyChanged("Selected");
            }
        }
        /// <summary>
        /// Deselects the DocObject.
        /// </summary>
        public virtual void DeSelect()
        {
            //If the flag is actually changing
            if (selectedFlag == true)
            {
                selectedFlag = false;

                if (SelectedChanged != null)
                {
                    SelectedChanged(this, selectedFlag);
                }
                RaisePropertyChanged("Selected");
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
        public event Action<object, bool> SelectedChanged;

        #endregion

        #region IInteractable: Edit, Remove, ContextMenu logic

        /// <summary>
        /// Gets the context menu for the DocObject.
        /// </summary>
        public abstract IContextMenu ContextMenu { get; }

        private bool editedFlag;

        /// <summary>
        /// Marks the DocObject as edited.
        /// </summary>
        public virtual void FlagAsEdited()
        {
            if (editedFlag == false)
            {
                editedFlag = true;
                if (EditedFlagChanged != null)
                {
                    EditedFlagChanged(this, editedFlag);
                }
                RaisePropertyChanged("EditedFlag");
            }
        }
        /// <summary>
        /// Gets if the DocObject has been edited.
        /// </summary>
        public bool EditedFlag
        {
            get { return editedFlag; }
        }
        /// <summary>
        /// Event that fires if the DocObjects EditedFlag has changed.
        /// </summary>
        public event Action<object, bool> EditedFlagChanged;

        private bool removeFlag;

        /// <summary>
        /// Flags the DocObject for removal. Will set RemoveFlag to true and call RemoveFlagChanged.
        /// </summary>
        public virtual void FlagForRemoval()
        {
            if (removeFlag == false)
            {
                removeFlag = true;

                DeSelect();
                
                if (RemoveFlagChanged != null)
                {
                    RemoveFlagChanged(this, removeFlag);
                }
                RaisePropertyChanged("RemoveFlag");
            }            
        }
        /// <summary>
        /// Gets the current flag for removal of the DocObject.
        /// </summary>
        public bool RemoveFlag
        {
            get { return removeFlag; }
        }
        /// <summary>
        /// Event that fires if the state of RemoveFlag changes.
        /// </summary>
        public event Action<object, bool> RemoveFlagChanged;

        #endregion

        #region IStorable: Input/Output (IO) logic

        public abstract string Error { get; }

        public abstract string this[string columnName] { get; }

        #endregion
    }
}
