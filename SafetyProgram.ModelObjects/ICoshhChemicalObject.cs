using System.ComponentModel;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.ModelObjects
{
    public interface ICoshhChemicalObject : 
        INotifyPropertyChanged, IStorable, ICopyPasteable, IDeepCloneable<ICoshhChemicalObject>
    {
        decimal Value { get; set; }
        string Unit { get; set; }
        IChemicalModelObject Chemical { get; set; }
    }
}
