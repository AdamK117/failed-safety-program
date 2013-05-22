using System.Collections.Generic;

namespace SafetyProgram.Base
{
    public static class ErrorValidation
    {
        public static string JoinErrors(IEnumerable<string> errorList)
        {
            string errors = "";

            foreach(string error in errorList)
            {
                errors += error + ", ";
            }

            return errors;
        }
    }
}
