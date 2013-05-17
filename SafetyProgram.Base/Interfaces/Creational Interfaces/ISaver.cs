namespace SafetyProgram.Base.Interfaces
{
    public interface ISaver<out TOutput, in TInput>
    {
        TOutput Store(TInput data);
    }
}
