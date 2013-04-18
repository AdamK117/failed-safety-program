using System;
namespace SafetyProgram.BaseClasses
{
    public interface IInteractable
    {
        IContextMenu ContextMenu { get; }

        void FlagForRemoval();
        bool RemoveFlag { get; }
        event Action<IInteractable, bool> RemoveFlagChanged;

        void Edited();
        bool EditedFlag { get; }
        event Action<IInteractable, bool> EditedChanged;
    }
}
