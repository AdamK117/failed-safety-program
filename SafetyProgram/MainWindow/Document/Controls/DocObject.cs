using System.Windows.Controls;
using Fluent;

namespace SafetyProgram.MainWindow.Document.Controls
{
    public abstract class DocObject : BaseINPC
    {
        public abstract UserControl View { get; }
        public abstract DocObjectRibbon Ribbon { get; }
        public abstract DocObjectContextMenu ContextMenu { get; }

        public abstract bool Selected { get; set; }
        public delegate void isSelectedChangedDelegate(DocObject docObject, bool selected);
        public abstract event isSelectedChangedDelegate SelectedChanged;

        public abstract bool RemoveFlag { get; set; }
        public delegate void removeFlagDelegate(DocObject docObject, bool removalFlag);
        public abstract event removeFlagDelegate RemoveFlagChanged;

        public abstract bool CanRemove();
        public abstract bool CanEdit();
    }
}
