using System;
using System.ComponentModel;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Core;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.Views.MainView.Default
{
    /// <summary>
    /// Defines a standard implementation of an <code>IMainViewModel</code>
    /// </summary>
    public sealed class MainViewModel : 
        IMainViewModel
    {
        public MainViewModel(IApplicationKernel model,
            Ribbon ribbonView,
            Func<IDocument, Control> documentViewFactory)
        {
            Helpers.NullCheck(model,
                ribbonView,
                documentViewFactory);

            this.RibbonView = ribbonView;

            model.DocumentChanged +=
                (s, e) =>
                {
                    this.ContentView = e.NewProperty == null ?
                        null :
                        documentViewFactory(e.NewProperty);
                };
            this.ContentView = model.Document == null ? 
                null : 
                documentViewFactory(model.Document);
        }

        /// <summary>
        /// Get the ribbon view.
        /// </summary>
        public Ribbon RibbonView { get; private set; }

        private Control contentView;

        /// <summary>
        /// Get the content view.
        /// </summary>
        public Control ContentView 
        {
            get { return contentView; }
            set
            {
                contentView = value;
                PropertyChanged.Raise(this, "ContentView");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
