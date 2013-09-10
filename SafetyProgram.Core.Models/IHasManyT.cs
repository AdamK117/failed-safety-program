using System.Collections.ObjectModel;

namespace SafetyProgram.Core.Models
{
    public interface IHasManyT<TContent> : IHasMany
        where TContent : IApplicationModel
    {
        new ObservableCollection<TContent> Content { get; }
    }
}