using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.MainWindow
{
    internal interface ICoshhWindowT<T> : 
        ICoshhWindow, 
        IWindow<T>
    { }
}
