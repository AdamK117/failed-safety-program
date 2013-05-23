using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Base;

namespace MockApp
{
    public interface IHolder
    {
        object Content { get; }
        event EventHandler<GenericPropertyChangedEventArg<object>> ContentChanged;
    }
}
