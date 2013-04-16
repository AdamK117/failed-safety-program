using SafetyProgram.BaseClasses;

namespace SafetyProgram.BaseClasses
{
    public class DocFormat : BaseINPC
    {
        private string width;
        private string height;

        public DocFormat(string width, string height)
        {
            this.width = width;
            this.height = height;
        }

        public string Width
        {
            get { return width; }
            set
            {
                width = value;
                RaisePropertyChanged("Width");
            }
        }

        public string Height
        {
            get { return height; }
            set
            {
                height = value;
                RaisePropertyChanged("Height");
            }
        }
    }
}
