namespace SafetyProgram.Base.Interfaces
{
    public interface IInputFactory<out TOutput, in TInput>
    {
        TOutput Store(TInput data);
    }
}
