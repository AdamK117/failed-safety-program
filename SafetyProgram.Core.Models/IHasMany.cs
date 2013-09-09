using System.Collections.ObjectModel;

namespace SafetyProgram.Core.Models
{
    public interface IHasMany<TContent>
        where TContent : IApplicationModel
    {
        ObservableCollection<TContent> Content { get; }
    }
}