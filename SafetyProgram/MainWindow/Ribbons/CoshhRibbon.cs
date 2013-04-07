using Fluent;
using SafetyProgram.MainWindow.Commands;
using SafetyProgram.MainWindow.Document;
using SafetyProgram.MainWindow.Document.Commands;
using SafetyProgram.MainWindow.Document.Controls;

namespace SafetyProgram.MainWindow.Ribbons
{
    public class CoshhRibbon : BaseINPC
    {
        private readonly CoshhRibbonView ribbon;
        private readonly CoshhWindow window;
        
        private RibbonTabItem currentContextualTab;

        /// <summary>
        /// Constructs a new instance of a ribbon for the CoshhProgram
        /// </summary>
        /// <param name="window"></param>
        public CoshhRibbon(CoshhWindow window)
        {
            this.window = window;
            ribbon = new CoshhRibbonView(this);

            //Event Handler: Triggers if the document changes.
            window.DocumentChanged += documentChanged;

            //Just incase theres already a document.
            if (window.Document != null) { documentChanged(window.Document); }
        }

        /// <summary>
        /// Gets the ribbon control.
        /// </summary>
        public CoshhRibbonView View
        {
            get { return ribbon; }
        }

        /// <summary>
        /// Gets a reference to the windows commands (Open, Close, New, etc.)
        /// </summary>
        public WindowCommandsHolder WindowCommands
        {
            get { return window.Commands; }
        }

        /// <summary>
        /// Gets the current documents commands.
        /// </summary>
        public DocumentCommandsHolder DocumentCommands
        {
            get { return window.Document.Commands; }
        }

        /// <summary>
        /// Sets the visibility of the ribbon (if ribbon buttons are clickable etc.). Will be false if there's no document.
        /// </summary>
        public bool RibbonVisibility 
        { 
            get { return window.Document == null ? false : true; } 
        }

        /// <summary>
        /// Handler thats called if the document changes (new, closed, loaded, etc.)
        /// </summary>
        /// <param name="document"></param>
        private void documentChanged(CoshhDocument document)
        {
            //If the document isn't null (i.e. closed).
            if (document != null)
            {
                //Event Handler: Added to the new document if the selection changes.
                document.SelectionChanged += (DocObject selection) =>
                {
                    View.Tabs.Remove(currentContextualTab);
                    if (selection != null)
                    {
                        View.Tabs.Add(currentContextualTab = selection.Ribbon.View);
                    }
                };

                //Get fresh set of commands for this document.
                RaisePropertyChanged("DocumentCommands");
            }

            //Gets rid of redundant tabs et. al.
            View.Tabs.Remove(currentContextualTab);
            RaisePropertyChanged("RibbonVisibility");
        }
    }
}
