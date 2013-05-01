using System.Collections.ObjectModel;
using System.ComponentModel;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.ModelObjects
{
    public interface IChemicalModelObject : 
        INotifyPropertyChanged, IStorable, ICopyPasteable, IDeepCloneable<IChemicalModelObject>
    {
        string Name { get; set; }
        ObservableCollection<IHazardModelObject> Hazards { get; }
    }
}
