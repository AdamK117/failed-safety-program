using System;
using System.Windows.Controls;
using System.Xml.Linq;
using SafetyProgram.BaseClasses;

namespace SafetyProgram.DocObjects
{
    public interface IDocObject : IViewable
    {
        IContextMenu ContextMenu { get; }

        IRibbonTabItem RibbonTab { get; }

        void Select();
        void DeSelect();
        bool Selected { get; }
        event Action<IDocObject, bool> SelectedChanged;

        void Edited();
        bool EditedFlag { get; }
        event Action<IDocObject, bool> EditedChanged;

        void FlagForRemoval();
        bool RemoveFlag { get; }
        event Action<IDocObject, bool> RemoveFlagChanged;

        void Load(XElement data);
        XElement Save();
    }
}
