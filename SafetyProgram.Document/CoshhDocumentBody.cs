using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document
{
    public sealed class CoshhDocumentBody : IDocumentBody, INotifyPropertyChanged
    {
        public CoshhDocumentBody(ObservableCollection<IDocumentObject> items)
        {
            Helpers.NullCheck(items);

            foreach (IDocumentObject item in items)
            {
                this.items.Add(item);
            }
            this.items.CollectionChanged += items_CollectionChanged;      
        }

        private readonly ObservableCollection<RibbonTabItem> contextualRibbonTabs = new ObservableCollection<RibbonTabItem>();
        public ObservableCollection<RibbonTabItem> ContextualRibbonTabs
        {
            get { return contextualRibbonTabs; }
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
                contextualRibbonTabs.Clear();
                contextualRibbonTabs.Add(selection.ContextualTab);

                SelectionChanged.Raise(this, selection);
                PropertyChanged.Raise(this, "Selection");
            }
            else throw new ArgumentNullException("Attempted to select nothing, use ClearSelection instead when attempting to clear a CoshhDocuments selection");
        }

        public void DeSelect(IDocumentObject item)
        {
            DeSelectAll();
            SelectionChanged.Raise(this, selection);
            PropertyChanged.Raise(this, "Selection");
        }

        public void DeSelectAll()
        {
            selection = null;
            contextualRibbonTabs.Clear();

            SelectionChanged.Raise(this, selection);
            PropertyChanged.Raise(this, "Selection");
        }

        public event EventHandler<GenericPropertyChangedEventArg<IDocumentObject>> SelectionChanged;

        void items_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                //If DocObject(s) are removed from the CoshhDocument.
                case NotifyCollectionChangedAction.Remove:
                    foreach (IDocumentObject control in e.OldItems)
                    {
                        DeSelect(control);
                    }
                    break;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
