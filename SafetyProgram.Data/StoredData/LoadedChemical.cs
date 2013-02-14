using SafetyProgram.Models.DataModels;
using Microsoft.Practices.ServiceLocation;

namespace SafetyProgram.Data.ChemicalData
{
    class LoadedChemical : IModelHolder
    {
        public ChemicalModel Model { get; set; }

        public LoadedChemical(ChemicalModel model) { this.Model = model; }

        public bool AddToActiveData()
        {
            ActiveCoshhData currentlyOpen = ServiceLocator.Current.GetInstance<ActiveCoshhData>();
            return currentlyOpen.Factory.Add(Model);
        }

        public void Edit()
        {
            //Open an edit window
        }
    }
}
