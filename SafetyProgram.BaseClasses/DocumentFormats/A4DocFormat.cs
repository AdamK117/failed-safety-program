namespace SafetyProgram.BaseClasses.DocumentFormats
{
    public class A4DocFormat : IDocFormat
    {
        public A4DocFormat() { }

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
