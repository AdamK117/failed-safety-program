using System.ComponentModel;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.ModelObjects
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
