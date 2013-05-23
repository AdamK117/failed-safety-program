using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using SafetyProgram.Base.Interfaces;

namespace MockApp.Document
{
    public sealed class DocumentViewModel : IDocumentViewModel
    {
        public DocumentViewModel(ContextMenu contextMenu, 
            ObservableCollection<Control> content, 
            IFormat format)
        {
            if (contextMenu == null ||
                content == null ||
                format == null)
            {
                throw new ArgumentNullException();
            }
            else
            {
                this.contextMenu = contextMenu;
            }
        }

        private readonly ContextMenu contextMenu;
        public ContextMenu ContextMenu
        {
            get { return contextMenu; }
        }

        private readonly ObservableCollection<Control> content;
        public ObservableCollection<Control> Content
        {
            get { return content; }
        }

        private readonly IFormat format;
        public IFormat Format
        {
            get { return format; }
        }
    }
}
