using SafetyProgram.BaseClasses;

namespace SafetyProgram.BaseClasses
{
    public class CustomDocFormat : IDocFormat
    {
        private string width;
        private string height;

        public CustomDocFormat(string width, string height)
        {
            this.width = width;
            this.height = height;
        }

        public string Width
        {
            get { return width; }
        }

        public string Height
        {
            get { return height; }
        }
    }
}
