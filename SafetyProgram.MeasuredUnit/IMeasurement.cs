using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.MeasuredUnit
{
    public interface IMeasurement
    {
        decimal Value { get; }
        IEnumerable<IUnit> Units { get; }
    }
}
