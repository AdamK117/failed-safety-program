using System.Collections.ObjectModel;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.ModelObjects
{
    public static class ModelObjectsPrototypes
    {
        public static IChemicalModelObject ChemicalModelObject()
        {
            return new ChemicalModelObject("", new ObservableCollection<IHazardModelObject>());
        }

        public static ICoshhChemicalObject CoshhChemicalObject()
        {
            return new CoshhChemicalObject(0M, "mgs", ChemicalModelObject());
        }

        public static IHazardModelObject HazardModelObject()
        {
            return new HazardModelObject("", "", "");
        }

        public static ICoshhChemicalObject CoshhChemicalObject(decimal amount, string units, IChemicalModelObject chemical)
        {
            return new CoshhChemicalObject(amount, units, chemical);
        }
    }
}
