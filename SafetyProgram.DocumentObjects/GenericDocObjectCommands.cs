using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.DocumentObjects
{
    public static class GenericDocObjectCommands
    {
        public static void Raise(this Action<object, bool> handler, object sender, bool payload)
        {
            if (handler != null)
            {
                handler(sender, payload);
            }
        }
    }
}
