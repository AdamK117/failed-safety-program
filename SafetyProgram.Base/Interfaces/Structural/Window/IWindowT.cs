using System;

namespace SafetyProgram.Base.Interfaces
{
    /// <summary>
    /// Defines an IWindow that can house another, servicable, object (e.g. an IDocument)
    /// </summary>
    /// <typeparam name="TContent">An IDocument that the IWindow houses</typeparam>
    public interface IWindow<TContent> : 
        IWindow, 
        IContentWindow
    {
        new TContent Content { get; set; }
        event EventHandler<GenericPropertyChangedEventArg<TContent>> ContentChanged;

        IIOService<TContent> Service { get; }
        void ChangeService(IIOService<TContent> newService);
        event EventHandler<GenericPropertyChangedEventArg<IIOService<TContent>>> ServiceChanged;
    }
}
