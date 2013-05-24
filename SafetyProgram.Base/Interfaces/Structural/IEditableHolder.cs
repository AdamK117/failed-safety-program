namespace SafetyProgram.Base.Interfaces
{
    public interface IEditableHolder<TContent> : IHolder<TContent>
    {
        new TContent Content { get; set; }
    }
}
