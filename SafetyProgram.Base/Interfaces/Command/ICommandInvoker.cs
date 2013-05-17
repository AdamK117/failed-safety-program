using System;

namespace SafetyProgram.Base.Interfaces
{
    public interface ICommandInvoker
    {
        void InvokeCommand(IInvokedCommand command);

        void Undo();
        bool CanUndo();
        event EventHandler<GenericPropertyChangedEventArg<bool>> CanUndoChanged;

        void Redo();
        bool CanRedo();
        event EventHandler<GenericPropertyChangedEventArg<bool>> CanRedoChanged;        
    }
}
