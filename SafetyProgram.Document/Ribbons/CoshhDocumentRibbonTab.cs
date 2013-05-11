using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Commands;

namespace SafetyProgram.Document.Ribbons
{
    public sealed class CoshhDocumentRibbonTab : IRibbonTabItem
    {
        private readonly CoshhDocument document;  

        public CoshhDocumentRibbonTab(CoshhDocument document)
        {
            this.document = document;
            view = new CoshhDocumentRibbonTabView(this);
        }

        public DocumentICommands DocumentCommands
        {
            get 
            {
                return document.Commands; 
            }
        }

        private readonly CoshhDocumentRibbonTabView view;
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
