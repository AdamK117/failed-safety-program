using System.Collections.Generic;

namespace SafetyProgram.MeasuredUnit
{
    public class Measurement : IMeasurement
    {
        public Measurement(decimal value, IUnit unit)
        {
            Value = value;
            Units = new List<IUnit>() { unit };
        }

        public Measurement(decimal value, IEnumerable<IUnit> units)
        {
            Value = value;
            Units = units;
        }

        public Measurement(decimal value, params IUnit[] units)
        {
            Value = value;
            Units = units;
        }

        public decimal Value
        {
            get;
            private set;
        }

        public IEnumerable<IUnit> Units
        {
            get;
            private set;
        }
    }
}
