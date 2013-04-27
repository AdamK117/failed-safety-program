using System;

namespace SafetyProgram.Models
{
    [Serializable]
    public sealed class ChemicalCoshhEntryModel
    {
        public decimal Value
        {
            get;
            set;
        }

        public string Unit
        {
            get;
            set;
        }

        private ChemicalModel chemical = new ChemicalModel();
        public ChemicalModel Chemical
        {
            get { return chemical; }
            set { chemical = value; }
        }
    }
}