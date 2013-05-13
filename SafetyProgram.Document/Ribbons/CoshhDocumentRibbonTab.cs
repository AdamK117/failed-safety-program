using System;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Commands;

namespace SafetyProgram.Document.Ribbons
{
    public sealed class CoshhDocumentRibbonTab : IRibbonTabItem
    {
        private readonly ICoshhDocument document;  

        public CoshhDocumentRibbonTab(
            ICoshhDocument document,
            Func<CoshhDocumentRibbonTab, RibbonTabItem> viewConstructor
            )
        {
            this.document = document;
            view = viewConstructor(this);
        }

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
