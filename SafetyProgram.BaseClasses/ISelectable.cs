using System;

namespace SafetyProgram.BaseClasses
{
    public interface ISelectable : IViewable, IInteractable
    {
        IRibbonTabItem RibbonTab { get; }

        void Select();
        void DeSelect();
        bool Selected { get; }
        event Action<ISelectable, bool> SelectedChanged;
    }
}
