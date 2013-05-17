using System;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Commands;

namespace SafetyProgram.Document.Ribbons
{
    internal sealed class CoshhDocumentRibbonTab : ICoshhDocumentRibbonTab
    {
        public CoshhDocumentRibbonTab(
            ICoshhDocument document,
            Func<ICoshhDocumentRibbonTab, RibbonTabItem> viewConstructor
            )
        {
            if (document != null && viewConstructor != null)
            {
                this.document = document;
                view = viewConstructor(this);
            }
            else throw new ArgumentNullException();
        }

        private readonly ICoshhDocument document;
        public IDocumentICommands DocumentCommands
        {
            get 
            {
                return document.Commands; 
            }
        }

        private readonly RibbonTabItem view;
        public RibbonTabItem View
        {
            get 
            { 
                return view; 
            }
        }
        Control IViewable.View
        {
            get 
            { 
                return view; 
            }
        }
    }
}
