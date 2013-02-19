using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SafetyProgram.UserControls
{
    public interface IDocInteractable
    {
        bool canRemove();
        bool Remove();

        bool canEdit();
        bool Edit();

        bool CanSelect();
        bool Selected();
        bool Selected(bool isSelected);
    }
}
