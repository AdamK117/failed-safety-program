using System.Collections.Generic;

namespace SafetyProgram.Data.ChemicalData
{
    interface IModelsHolder
    {
        List<IModelHolder> models();

        bool Search(string searchString);
        bool Refresh();
        bool Load(string path, string search);
        bool Delete(object model);
    }
}
