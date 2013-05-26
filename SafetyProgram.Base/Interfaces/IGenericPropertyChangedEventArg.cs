namespace SafetyProgram.Base
{
    public interface IGenericPropertyChangedEventArg<out T>
    {
        T NewProperty { get; }
    }
}
