using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentUi
{
    public sealed class CoshhDocumentViewModel : ICoshhDocumentViewModel
    {
        public CoshhDocumentViewModel(IHolder<IFormat> formatHolder,
            ContextMenu contextMenu,
            IDocumentBody documentBody,
            List<InputBinding> hotkeys)
        {
            Helpers.NullCheck(formatHolder, contextMenu, documentBody, hotkeys);

            this.formatHolder = formatHolder;
            this.contextMenu = contextMenu;
            this.documentBody = documentBody;
            this.hotkeys = hotkeys;

            this.formatHolder.ContentChanged += (sender, newFormat) => PropertyChanged.Raise(this, "Format");
        }

        private readonly IHolder<IFormat> formatHolder;
        public IFormat Format
        {
            get { return formatHolder.Content; }
        }

        private readonly ContextMenu contextMenu;
        public ContextMenu ContextMenu
        {
            get { return contextMenu; }
        }

        private readonly IDocumentBody documentBody;
        public IDocumentBody Body
        {
            get { return documentBody; }
        }

        private readonly List<InputBinding> hotkeys;
        public List<InputBinding> Hotkeys
        {
            get { return hotkeys; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
