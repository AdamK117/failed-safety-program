using System;

namespace SafetyProgram.Base.Interfaces
{
    /// <summary>
    /// Defines a IViewable object that is interactable:
    /// -It can be right clicked and has a context menu
    /// -It can be removed
    /// -It can be edited
    /// </summary>
    public interface IInteractable : IViewable
    {
        /// <summary>
        /// Gets the context menu (IContextMenu) viewmodel for this IInteractable object
        /// </summary>
        IContextMenu ContextMenu { get; }

        /// <summary>
        /// Flags this IInteractable object for removal
        /// </summary>
        void FlagForRemoval();
        /// <summary>
        /// Gets the if this IInteractable has been flagged for removal.
        /// </summary>
        bool RemoveFlag { get; }
        /// <summary>
        /// An event that triggers if the RemoveFlag of this IInteractable has changed.
        /// </summary>
        event Action<object, bool> RemoveFlagChanged;

        /// <summary>
        /// Flags this IInteractable as edited.
        /// </summary>
        void FlagAsEdited();
        /// <summary>
        /// Gets the edited state of the IInteractable, indicating if it has been edited.
        /// </summary>
        bool EditedFlag { get; }
        /// <summary>
        /// An event that fires if the IInteractable's EditedFlag changes
        /// </summary>
        event Action<object, bool> EditedFlagChanged;
    }
}
