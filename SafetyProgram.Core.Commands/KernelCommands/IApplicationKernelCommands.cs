using System.Collections.Generic;

namespace SafetyProgram.Core.Commands.KernelCommands
{
    public interface IApplicationKernelCommands
    {
        void Insert<TParent, TChild>(TParent parent, TChild newItem)
            where TParent : ICompositeNode<TChild>;

        void Delete<TParent, TChild>(TParent parent, TChild item)
            where TParent : ICompositeNode<TChild>;

        void CopySelection();

        void Paste();

        void DeleteSelection();
    }
}
