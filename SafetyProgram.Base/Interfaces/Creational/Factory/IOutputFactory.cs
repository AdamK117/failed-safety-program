namespace SafetyProgram.Base.Interfaces
{
    public interface IOutputFactory<out TOutput, in TInput>
    {
        TOutput Load(TInput data);
    }
}
