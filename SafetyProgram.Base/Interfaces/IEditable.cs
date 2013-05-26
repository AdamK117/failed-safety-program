using System;

namespace SafetyProgram.Base.Interfaces
{
    public interface IEditable
    {
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
        event EventHandler<GenericPropertyChangedEventArg<bool>> EditedFlagChanged;
    }
}
