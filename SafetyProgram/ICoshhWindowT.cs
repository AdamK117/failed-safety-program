using SafetyProgram.Base.Interfaces;

namespace SafetyProgram
{
    internal interface ICoshhWindowT<T> : 
        ICoshhWindow, 
        IWindow<T>
    { }
}
