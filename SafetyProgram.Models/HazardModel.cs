using System;

namespace SafetyProgram.Models
{
    [Serializable]
    public sealed class HazardModel
    {
        public string Hazard
        {
            get;
            set;
        }

        public string SignalWord
        {
            get;
            set;
        }

        public string Symbol
        {
            get;
            set;
        }
    }
}
