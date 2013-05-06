using System.ComponentModel;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.ModelObjects
{
    public interface IHazardModelObject :
        INotifyPropertyChanged, IStorable<IHazardModelObject>, ICopyPasteable, IDeepCloneable<IHazardModelObject>
    {
        string Hazard { get; set; }
        string SignalWord { get; set; }
        string Symbol { get; set; }
    }
}
