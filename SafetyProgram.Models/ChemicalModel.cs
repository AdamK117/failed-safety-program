using System;
using System.Collections.Generic;

namespace SafetyProgram.Models
{
    [Serializable]
    public sealed class ChemicalModel
    {
        public string Name
        {
            get;
            set;
        }

        private IList<HazardModel> hazards = new List<HazardModel>();
        public IList<HazardModel> Hazards
        {
            get { return hazards; }
            set { hazards = value; }
        }
    }
}
