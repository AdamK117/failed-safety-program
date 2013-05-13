using System;

namespace SafetyProgram.DocumentObjects
{
    internal static class GenericDocObjectCommands
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
