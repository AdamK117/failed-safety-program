using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace SafetyProgram.UserControls.TagList.StandardTagList
{
    public sealed class StandardTagList : TagListView, ITagList, INotifyPropertyChanged
    {
        public ICommand RemoveItemCommand { get; private set; }
        public Control View { get { return this; } }

        public StandardTagList() : base()
        {
            RemoveItemCommand = new RemoveTagListItem();
            LayoutRoot.DataContext = this;
        }

        public static readonly DependencyProperty BoxItemsProperty = DependencyProperty.Register
            (
                "BoxItems",
                typeof(ObservableCollection<object>),
                typeof(StandardTagList),
                new FrameworkPropertyMetadata
                    (
                        new ObservableCollection<object>(),
                        FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                    )
            );

        public ObservableCollection<object> BoxItems
        {
            get
            {
                return (ObservableCollection<object>)GetValue(BoxItemsProperty);
            }
            set { SetValue(BoxItemsProperty, value); }
        }

        public ObservableCollection<ITagListItem> Items
        {
            get
            {
                BoxItems.CollectionChanged += (sender, e) => PropertyChanged(this, new PropertyChangedEventArgs("Items"));

                return new ObservableCollection<ITagListItem>(
                    BoxItems.Select(
                        item => new TagListItem(
                            item.ToString(), 
                            ()=>BoxItems.Remove(item),
                            null
                        )
                    )
                );
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
