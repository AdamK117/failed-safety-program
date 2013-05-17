namespace SafetyProgram.Base.Interfaces
{
    public interface ILoader<out TOutput, in TInput>
    {
        TOutput Load(TInput data);
    }
}
