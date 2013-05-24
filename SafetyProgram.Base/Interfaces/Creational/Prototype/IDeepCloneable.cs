namespace SafetyProgram.Base.Interfaces
{
    public interface IDeepCloneable<out T>
    {
        T DeepClone();
    }
}
