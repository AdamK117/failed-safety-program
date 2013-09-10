using System.Collections.Generic;

namespace SafetyProgram.Core.Models
{
    public interface IHasMany : IApplicationModel
    {
        IEnumerable<IApplicationModel> Content { get; }
    }
}
