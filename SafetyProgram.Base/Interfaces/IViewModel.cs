using System;
namespace SafetyProgram.Base
{
    public interface IViewModel<T>
    {
        T Model { get; }
    }

    public static class IViewModelExtns
    {
        public static IViewModel<T2> Bind<T1,T2>(this IViewModel<T1> viewModel, Func<T1, IViewModel<T2>> binder)
        {
            return binder(viewModel.Model);
        }
    }
}
