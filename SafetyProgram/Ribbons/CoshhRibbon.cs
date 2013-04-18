using Fluent;

using SafetyProgram.BaseClasses;
using SafetyProgram.Commands;
using SafetyProgram.DocObjects;
using SafetyProgram.Document;
using SafetyProgram.Document.Commands;

namespace SafetyProgram.Ribbons
{
    public class CoshhRibbon : BaseINPC
    {
        private readonly CoshhWindow window;
        private readonly CoshhRibbonView ribbon;
        
        private RibbonTabItem currentContextualTab;
        private DocumentCommandsHolder documentCommands;

        /// <summary>
        /// Constructs the CoshhWindow's ribbon. This is the primary ribbon for the CoshhWindow.
        /// </summary>
        /// <param name="window">CoshhWindow the ribbon is a child of.</param>
        public CoshhRibbon(CoshhWindow window)
        {
            this.window = window;            

            ribbon = new CoshhRibbonView(this);

            //Monitor if the CoshhDocument in the CoshhWindow changes.
            window.DocumentChanged += documentChanged;

            //Prematurely trigger the handler if a CoshhDocument is already open in the CoshhWindow
            if (window.Document != null) documentChanged(window.Document);
        }

        /// <summary>
        /// Gets the CoshhRibbon view.
        /// </summary>
        public CoshhRibbonView View
        {
            get 
            { 
                return ribbon; 
            }
        }

        /// <summary>
        /// Gets the parent CoshhWindow's commands.
        /// </summary>
        public WindowCommandsHolder WindowCommands
        {
            get 
            { 
                return window.Commands; 
            }
        }

        /// <summary>
        /// Gets the current CoshhDocument (within the CoshhWindow) commands
        /// </summary>
        public DocumentCommandsHolder DocumentCommands
        {
            get 
            { 
                return documentCommands; 
            }
        }

        /// <summary>
        /// Gets the visibility state of the CoshhRibbon. False if there is no CoshhDocument present in the CoshhWindow.
        /// </summary>
        public bool RibbonVisibility 
        { 
            get 
            { 
                return window.Document == null ? false : true; 
            } 
        }

        /// <summary>
        /// Handler that is called if the CoshhDocument contained within the CoshhWindow changes.
        /// </summary>
        /// <param name="document">The new CoshhDocument (or lack of).</param>
        private void documentChanged(ICoshhDocument document)
        {
            //If the new document is not closed.
            if (document != null)
            {
                //Add an event handler to monitor the new documents selections.
                document.SelectionChanged += (IDocObject selection) =>
                {
                    //If the selection is null (i.e. deselected) or if the new Ribbon tab is new, remove the old RibbonTab.
                    if (selection == null || currentContextualTab != selection.RibbonTab.View)
                    {
                        View.Tabs.Remove(currentContextualTab);
                    }
                    
                    if (selection != null)
                    {
                        View.Tabs.Add(currentContextualTab = selection.RibbonTab.View);
                    }  
                };

                //Get a fresh DocumentCommandsHolder from the new CoshhDocument
                documentCommands = window.Document.Commands;
                RaisePropertyChanged("DocumentCommands");
            }

            //Remove contextual RibbonTab (might be from closed CoshhDocument).
            View.Tabs.Remove(currentContextualTab);
            RaisePropertyChanged("RibbonVisibility");
        }
    }
}
