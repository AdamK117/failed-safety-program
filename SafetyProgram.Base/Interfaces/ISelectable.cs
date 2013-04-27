using System;

namespace SafetyProgram.Base.Interfaces
{
    /// <summary>
    /// Defines an interface for an object that is selectable:
    /// -It can be viewed (IViewable)
    /// -Right clicked, edited, and removed (IInteractable)
    /// -May be selected/deselected
    /// -Has a contextual ribbon for when it is selected/deselected
    /// </summary>
    public interface ISelectable : IInteractable
    {
        /// <summary>
        /// Gets the contextual IRibbonTabItem associated with this ISelectable
        /// </summary>
        IRibbonTabItem RibbonTab { get; }

        /// <summary>
        /// Selects this ISelectable
        /// </summary>
        void Select();

        /// <summary>
        /// DeSelects this ISelectable
        /// </summary>
        void DeSelect();

        /// <summary>
        /// Gets if this ISelectable is currently selected
        /// </summary>
        bool Selected { get; }

        /// <summary>
        /// Event that triggers if the Selected state of this ISelectable changes
        /// </summary>
        event Action<object, bool> SelectedChanged;
    }
}
