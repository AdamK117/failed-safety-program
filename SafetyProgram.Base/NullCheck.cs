using System;

namespace SafetyProgram.Base
{
    public static class Helpers
    {
        public static bool NullCheck(params object[] items)
        {
            foreach (object item in items)
            {
                if (item == null) throw new ArgumentNullException();
            }

            return true;
        }
    }
}
