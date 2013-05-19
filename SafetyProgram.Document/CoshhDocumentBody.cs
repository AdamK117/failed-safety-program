using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document.Body
{
    public sealed class CoshhDocumentBody : 
        INotifyPropertyChanged,
        IDocumentBody
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
                SelectionChanged.Raise(this, selection);
                PropertyChanged.Raise(this, "Selection");
            }
            else throw new ArgumentNullException("Attempted to select nothing, use ClearSelection instead when attempting to clear a CoshhDocuments selection");
        }

        public void DeSelect(IDocumentObject item)
        {
            DeSelectAll();
            SelectionChanged.Raise(this, selection);
            PropertyChanged.Raise(this, "Selected");
        }

        public void DeSelectAll()
        {
            selection = null;
            SelectionChanged.Raise(this, selection);
            PropertyChanged.Raise(this, "Selection");
        }

        public event EventHandler<GenericPropertyChangedEventArg<IDocumentObject>> SelectionChanged;

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
                        control.RemoveFlagChanged += (object docObject, GenericPropertyChangedEventArg<bool> flag) =>
                        {
                            if (flag.NewProperty == true)
                            {
                                DeSelect(control);
                                items.Remove(control);
                            }
                        };
                        //Monitor the DocObjet's Selected flag.
                        control.SelectedChanged += (object docObject, GenericPropertyChangedEventArg<bool> flag) =>
                        {
                            if (flag.NewProperty == true)
                            {
                                Select(control);
                            }
                            else DeSelect(control);
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
