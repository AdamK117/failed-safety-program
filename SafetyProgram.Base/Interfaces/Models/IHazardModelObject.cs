using System.ComponentModel;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Base.Interfaces
{
    public interface IHazardModelObject :
        INotifyPropertyChanged, 
        ICopyPasteable, 
        IDeepCloneable<IHazardModelObject>, 
        IDataErrorInfo
    {
        string Hazard { get; set; }
        string SignalWord { get; set; }
        string Symbol { get; set; }
    }
}
