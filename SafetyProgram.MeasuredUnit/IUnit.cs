using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.MeasuredUnit
{
    public interface IUnit
    {
        IUnitType UnitType { get; }
        int Indicie { get; }
        decimal SiScale { get; }
    }
}
