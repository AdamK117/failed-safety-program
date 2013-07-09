using System.Collections.ObjectModel;

namespace SafetyProgram.DOM
{
    public interface IWindow
    {
        /// <summary>
        /// Gets the IDocs associated with this IWindow.
        /// </summary>
        ObservableCollection<IDoc> Documents { get; }
    }
}
