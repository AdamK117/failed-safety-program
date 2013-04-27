using System.Collections.ObjectModel;
using System.Windows.Input;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.UserControls.TagList
{
    public interface ITagList : IViewable
    {
        ObservableCollection<ITagListItem> Items { get; }
        ICommand RemoveItemCommand { get; }
    }
}
