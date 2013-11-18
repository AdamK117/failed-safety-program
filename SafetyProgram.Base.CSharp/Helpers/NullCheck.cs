using System;

namespace SafetyProgram.Base.CSharp
{
    /// <summary>
    /// Defines helper methods to be used in the rest of the program.
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Performs a null check on the supplied objects.
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static void NullCheck(params object[] items)
        {
            foreach (object item in items)
            {
                if (item == null) throw new ArgumentNullException();
            }
        }
    }
}
