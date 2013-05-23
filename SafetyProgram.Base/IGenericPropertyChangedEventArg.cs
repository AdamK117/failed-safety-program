using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.Base
{
    public interface IGenericPropertyChangedEventArg<out T>
    {
        T NewProperty { get; }
    }
}
