using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Data.ChemicalData
{
    public interface IModelHolder
    {
        bool AddToActiveData();
        void Edit();
    }
}
