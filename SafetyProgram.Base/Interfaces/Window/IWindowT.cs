using System;

namespace SafetyProgram.Base.Interfaces
{
    /// <summary>
    /// Defines an IWindow that can house another, servicable, object (e.g. an IDocument)
    /// </summary>
    /// <typeparam name="Doc">An IDocument that the IWindow houses</typeparam>
    public interface IWindow<Doc> : IWindow
        where Doc : IViewable, IStorable
    {
        Doc Content { get; set; }
        event Action<Doc> DocumentChanged;

        IService<Doc> Service { get; }
        void ChangeService(IService<Doc> newService);
        event Action<IService<Doc>> ServiceChanged;
    }
}
