using System.Windows.Controls;
using System.Xml;
using System.Collections.Generic;
using System.Xml.Linq;

namespace SafetyProgram.MainWindow.Document.Controls
{
    /// <summary>
    /// Defines an interface for IDocObjects. These objects act as containers for Document content.
    /// </summary>
    public interface IDocObject
    {
        /// <summary>
        /// Displays the IDocObjects usercontrol (e.g. a Chemical Table).
        /// </summary>
        /// <returns>The relevant user control.</returns>
        UserControl Display();

        bool canRemove();
        bool Remove();

        bool canEdit();
        bool Edit();

        bool CanSelect();
        bool IsSelected();
        void Select();
        void DeSelect();

        XElement IOSaveXml();
        IDocObject IOLoadXml(XDocument data);
    }
}
