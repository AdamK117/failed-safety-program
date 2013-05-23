using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Base;

namespace MockApp
{
    public interface IHolderT<out TContent> : IHolder
    {
        new TContent Content { get; }
        new event Action<object, TContent> ContentChanged; 
    }
}
