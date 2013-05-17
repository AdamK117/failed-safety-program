using SafetyProgram.Base.Interfaces;
namespace SafetyProgram.Base.DocumentFormats
{
    public class A4Format : IFormat
    {
        public string Width
        {
            get { return "630"; }
        }

        public string Height
        {
            get { return "891"; }
        }
    }
}
