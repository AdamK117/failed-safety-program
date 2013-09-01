using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core.Commands;
using SafetyProgram.UI.Document;

namespace SafetyProgram.UI
{
    public sealed class MainViewModel : IMainViewModel
    {
        private readonly IHolder<IDocumentController> documentUiControllerHolder;
        private readonly ICoreCommands coreCommands;

        /// <summary>
        /// Construct an instance of a main window viewmodel. A view model for the main application window.
        /// </summary>
        /// <param name="coreCommands">The core commands for the program (new, open, close, etc.)</param>
        /// <param name="documentUiController">The document ui controller for the current document.</param>
        public MainViewModel(ICoreCommands coreCommands, 
            IHolder<IDocumentController> documentUiControllerHolder, 
            Ribbon ribbonView)
        {
            this.coreCommands = coreCommands;
            this.documentUiControllerHolder = documentUiControllerHolder;
            this.ribbonView = ribbonView;

            documentUiControllerHolder.ContentChanged += (sender, newController) => PropertyChanged.Raise(this, "ContentView");
        }

        private readonly Ribbon ribbonView;
        
        /// <summary>
        /// Get the ribbon view for the main window.
        /// </summary>
        public Ribbon RibbonView
        {
            get { return ribbonView; }
        }

        private Control contentView;

        /// <summary>
        /// Get the content (document) view of the window.
        /// </summary>
        public Control ContentView
        {
            get 
            {
                var content = documentUiControllerHolder.Content;

                return (content == null) ? null : content.View;
            }
        }        

        /// <summary>
        /// Get the hotkeys associated with the main window view.
        /// </summary>
        public List<InputBinding> Hotkeys
        {
            get { return coreCommands.Hotkeys; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
