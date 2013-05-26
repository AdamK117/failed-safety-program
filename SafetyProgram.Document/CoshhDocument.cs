using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document
{
    public sealed class CoshhDocument : ICoshhDocument
    {
        public CoshhDocument(Control view,
            IDocumentBody body,
            IHolder<IFormat> formatHolder,
            ICollection<RibbonTabItem> ribbonTabs)
        {
            Helpers.NullCheck(view, body, formatHolder, ribbonTabs);

            this.view = view;
            this.body = body;
            this.formatHolder = formatHolder;
            this.ribbonTabs = ribbonTabs;

            this.contextualRibbonTabs = body.ContextualRibbonTabs;
        }

        private readonly Control view;
        public Control View
        {
            get { return view; }
        }

        private readonly IDocumentBody body;
        public IDocumentBody Body
        {
            get { return body; }
        }

        private readonly IHolder<IFormat> formatHolder;
        public IFormat Format
        {
            get { return formatHolder.Content; }
        }

        private readonly ICollection<RibbonTabItem> ribbonTabs;
        public ICollection<RibbonTabItem> DocumentRibbonTabs
        {
            get { return ribbonTabs; }
        }

        private readonly ObservableCollection<RibbonTabItem> contextualRibbonTabs;
        public ObservableCollection<RibbonTabItem> ContextualRibbonTabs
        {
            get { return contextualRibbonTabs; }
        }
    }
}
