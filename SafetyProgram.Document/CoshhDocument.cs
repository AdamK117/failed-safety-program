using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.Document
{
    public sealed class CoshhDocument : ICoshhDocument
    {
        public CoshhDocument(Control view,
            IDocumentBody body,
            IHolder<IFormat> formatHolder,
            ObservableCollection<RibbonTabItem> ribbonTabs)
        {
            if (view == null ||
                body == null ||
                formatHolder == null ||
                ribbonTabs == null)
                throw new ArgumentNullException();
            else
            {
                this.view = view;
                this.body = body;
                this.formatHolder = formatHolder;
                this.ribbonTabs = ribbonTabs;
            }
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

        private readonly ObservableCollection<RibbonTabItem> ribbonTabs;
        public ObservableCollection<RibbonTabItem> RibbonTabs
        {
            get { return ribbonTabs; }
        }
    }
}
