using System.Collections.Generic;

namespace SafetyProgram.Core.Commands
{
    public interface ICompositeNode<T>
    {
        ICollection<T> Content { get; }
    }
}
