namespace SafetyProgram.Base.Interfaces
{
    public interface IFactory <out T>
    {
        T CreateNew();
    }
}
