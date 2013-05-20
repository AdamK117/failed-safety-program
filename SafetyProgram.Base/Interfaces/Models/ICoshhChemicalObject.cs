using System.ComponentModel;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Base.Interfaces
{
    public interface ICoshhChemicalObject :
        INotifyPropertyChanged, 
        ICopyPasteable, 
        IDeepCloneable<ICoshhChemicalObject>, 
        IDataErrorInfo
    {
        decimal Value { get; set; }
        string Unit { get; set; }
        IChemicalModelObject Chemical { get; }
    }
}
