using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document.Body
{
    public sealed class CoshhDocumentBody : BaseINPC, IDocumentBody
    {
        /// <summary>
        /// Construct a blank CoshhDocumentBody
        /// </summary>
        public CoshhDocumentBody()
        {
            items.CollectionChanged += items_CollectionChanged;
        }

        /// <summary>
        /// Construct a CoshhDocumentBody containing the supplied IDocObject items
        /// </summary>
        /// <param name="items">Items to populate into this CoshhDocumentBody</param>
        public CoshhDocumentBody(IEnumerable<IDocumentObject> items)
        {
            foreach (IDocumentObject item in items)
            {
                this.items.Add(item);
            }
            this.items.CollectionChanged += items_CollectionChanged;
        }

        private readonly ObservableCollection<IDocumentObject> items = new ObservableCollection<IDocumentObject>();
        public ObservableCollection<IDocumentObject> Items
        {
            get 
            { 
                return items; 
            }
        }

        private IDocumentObject selection = null;
        public IDocumentObject Selection
        {
            get 
            { 
                return selection; 
            }
        }

        public void Select(IDocumentObject item)
        {
            if (item != null)
            {
                selection = item;

                if (SelectionChanged != null)
                {
                    SelectionChanged(selection);
                }
                RaisePropertyChanged("Selection");
            }
            else throw new ArgumentNullException("Attempted to select nothing, use ClearSelection instead when attempting to clear a CoshhDocuments selection");
        }

        public void DeSelect(IDocumentObject item)
        {
            //TODO: Multiselection logic so it can deselect the item provided.
            DeSelectAll();

            if (SelectionChanged != null)
            {
                SelectionChanged(selection);
            }
            RaisePropertyChanged("Selected");
        }

        public void DeSelectAll()
        {
            selection = null;

            if (SelectionChanged != null)
            {
                SelectionChanged(selection);
            }
            RaisePropertyChanged("Selection");
        }

        public event Action<IDocumentObject> SelectionChanged;

        void items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                #region Add
                //If new DocObject(s) were added to the Body.
                case NotifyCollectionChangedAction.Add:
                    foreach (IDocumentObject control in e.NewItems)
                    {
                        //Monitor the new DocObject's Remove flag
                        control.RemoveFlagChanged += (object docObject, bool flag) =>
                        {
                            if (flag == true)
                            {
                                DeSelect(control);
                                items.Remove(control);
                            }
                        };
                        //Monitor the DocObjet's Selected flag.
                        control.SelectedChanged += (object docObject, bool flag) =>
                        {
                            if (flag == true)
                            {
                                Select(control);
                            }
                            if (flag == false)
                            {
                                DeSelect(control);
                            }
                        };
                    }
                    break;
                #endregion

                #region Remove
                //If DocObject(s) are removed from the CoshhDocument.
                case NotifyCollectionChangedAction.Remove:
                    foreach (IDocumentObject control in e.OldItems)
                    {
                        DeSelect(control);
                    }
                    break;
                #endregion
            }
        }
    }
}
