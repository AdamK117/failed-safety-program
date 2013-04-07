using Fluent;

namespace SafetyProgram.MainWindow.Document.Controls
{
    public abstract class DocObjectRibbon : BaseINPC
    {
        public abstract RibbonTabItem View { get; }
    }
}
