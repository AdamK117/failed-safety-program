using Fluent;

namespace SafetyProgram.Base.Interfaces
{
    public interface IRibbon : 
        IViewable
    {
        new Ribbon View { get; }
    }
}
