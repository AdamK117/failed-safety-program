using System.Windows.Controls;

namespace SafetyProgram.UserControls
{
    public interface IDocDataHolder<out T> : IDocInteractable
    {
        T Data();
    }
}
