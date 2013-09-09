using SafetyProgram.Core.Models;

namespace SafetyProgram.Core.Commands.KernelCommands
{
    public interface IKernelCommands
    {
        bool CanUndo();

        void Undo();

        bool CanRedo();

        void Redo();

        bool CanDelete<T>(IHasMany<T> parent, T item)
            where T : IApplicationModel;

        void Delete<T>(IHasMany<T> parent, T item)
            where T : IApplicationModel;

        bool CanInsert<T>(IHasMany<T> parent, T item)
            where T : IApplicationModel;

        void Insert<T>(IHasMany<T> parent, T item)
            where T : IApplicationModel;

        bool CanInsert<T>(IHasMany<T> parent, T item, int index)
            where T : IApplicationModel;

        void Insert<T>(IHasMany<T> parent, T item, int index)
            where T : IApplicationModel;
    }
}
