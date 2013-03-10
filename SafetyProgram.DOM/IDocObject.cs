using System.Windows.Controls;

namespace SafetyProgram.UserControls
{
    public interface IDocObject
    {
        UserControl Display();

        bool canRemove();
        bool Remove();

        bool canEdit();
        bool Edit();

        bool CanSelect();
        bool Selected();
        bool Selected(bool selected);
    }
}
