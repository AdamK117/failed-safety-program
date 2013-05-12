using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.UserControls.TagList;

namespace SafetyProgram.UserControls.Generic.EnhancedTagList
{
    public sealed class EnhancedTagList<T> : INotifyPropertyChanged, ITagList
    {
        private readonly ObservableCollection<T> rawItems;
        private readonly Func<T, ITagListItem> binder;

        public ICommand RemoveItemCommand { get; private set; }
        public Control View { get; private set; }

        public EnhancedTagList(ObservableCollection<T> rawItems, Func<T, ITagListItem> binder)
        {
            this.rawItems = rawItems;
            this.binder = binder;

            RemoveItemCommand = new RemoveTagListItem();

            View = new TagListView(this);

            //TODO: Change this QAD refresher to a dynamic linker
            rawItems.CollectionChanged += (sender, e) => PropertyChanged.Raise(this, "Items");
        }        

        public ObservableCollection<ITagListItem> Items
        {
            get 
            {
                return new ObservableCollection<ITagListItem>(
                    from tli in rawItems
                    select binder(tli)
                );                
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
