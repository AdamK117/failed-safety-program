using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using SafetyProgram.BaseClasses;
using SafetyProgram.DocObjects;

namespace SafetyProgram.Document
{
    public class CoshhDocumentBody : BaseINPC, ICoshhDocumentBody
    {
        private readonly ObservableCollection<IDocObject> items;
        private IDocObject selection;

        /// <summary>
        /// Construct a blank CoshhDocumentBody
        /// </summary>
        public CoshhDocumentBody()
        {
            items = new ObservableCollection<IDocObject>();
            items.CollectionChanged += items_CollectionChanged;
        }

        /// <summary>
        /// Construct a CoshhDocumentBody containing the supplied IDocObject items
        /// </summary>
        /// <param name="items">Items to populate into this CoshhDocumentBody</param>
        public CoshhDocumentBody(IEnumerable<IDocObject> items)
        {
            this.items = new ObservableCollection<IDocObject>(items);
            this.items.CollectionChanged += items_CollectionChanged;
        }

        public ObservableCollection<IDocObject> Items
        {
            get { return items; }
        }

        public IDocObject Selection
        {
            get { return selection; }
        }

        public void Select(IDocObject item)
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

        public void DeSelect(IDocObject item)
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

        public event Action<IDocObject> SelectionChanged;

        void items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                #region Add
                //If new DocObject(s) were added to the Body.
                case NotifyCollectionChangedAction.Add:
                    foreach (IDocObject control in e.NewItems)
                    {
                        //Monitor the new DocObject's Remove flag
                        control.RemoveFlagChanged += (IDocObject docObject, bool flag) =>
                        {
                            if (flag == true)
                            {
                                DeSelect(docObject);
                                items.Remove(docObject);
                            }
                        };
                        //Monitor the DocObjet's Selected flag.
                        control.SelectedChanged += (IDocObject docObject, bool flag) =>
                        {
                            if (flag == true)
                            {
                                Select(docObject);
                            }
                            if (flag == false)
                            {
                                DeSelect(docObject);
                            }
                        };
                    }
                    break;
                #endregion

                #region Remove
                //If DocObject(s) are removed from the CoshhDocument.
                case NotifyCollectionChangedAction.Remove:
                    foreach (IDocObject control in e.OldItems)
                    {
                        DeSelect(control);
                    }
                    break;
                #endregion
            }
        }
    }
}
