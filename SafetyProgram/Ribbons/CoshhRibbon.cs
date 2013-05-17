using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Commands;

namespace SafetyProgram.Ribbons
{
    internal sealed class CoshhRibbon<TContent> : ICoshhRibbon
        where TContent : IDocument
    {
        private readonly ICoshhWindowT<TContent> window;

        /// <summary>
        /// Constructs the CoshhWindow's ribbon. This is the primary ribbon for the CoshhWindow.
        /// </summary>
        /// <param name="window">CoshhWindow the ribbon is a child of.</param>
        public CoshhRibbon(
            ICoshhWindowT<TContent> window, 
            Func<ICoshhRibbon, Ribbon> viewConstructor
            )
        {
            if (
                window != null &&
                viewConstructor != null
                )
            {
                this.window = window;

                this.view = viewConstructor(this);
                window.ContentChanged += windowContentChanged;
                if (window.Content != null) windowContentChanged(window, new GenericPropertyChangedEventArg<TContent>(window.Content));
            }
            else throw new ArgumentNullException();            
        }

        private readonly Ribbon view;
        /// <summary>
        /// Gets the CoshhRibbon view.
        /// </summary>
        public Ribbon View
        {
            get 
            {
                return view; 
            }
        }

        /// <summary>
        /// Get the CoshhRibbon view (IViewable)
        /// </summary>
        Control IViewable.View
        {
            get 
            { 
                return view; 
            }
        }

        /// <summary>
        /// Gets the parent CoshhWindow's commands.
        /// </summary>
        public IWindowCommands WindowCommands
        {
            get 
            { 
                return window.Commands; 
            }
        }

        private bool ribbonVisibility;
        /// <summary>
        /// Gets the visibility state of the CoshhRibbon. False if there is no CoshhDocument present in the CoshhWindow.
        /// </summary>
        public bool RibbonVisibility 
        {
            get 
            { 
                return ribbonVisibility; 
            }
            private set
            {
                ribbonVisibility = value;
                PropertyChanged.Raise(this, "RibbonVisibility");
            }
        }

        /// <summary>
        /// Handler that is called if the CoshhDocument contained within the CoshhWindow changes.
        /// </summary>
        /// <param name="content">The new CoshhDocument (or lack of).</param>
        private void windowContentChanged(object sender, GenericPropertyChangedEventArg<TContent> content)
        {
            // 2 Scenarios
            //  The CoshhWindow closed its IDocument (null): Clear data, lock ribbon.
            //  A new IDocument was set in the CoshhWindow: Add handlers, unlock ribbon (if it was locked).

            //The IDocument was closed
            if (content.NewProperty == null)
            {
                //TODO:Greying effect
                RibbonVisibility = false;
                View.Tabs.Clear();
            }
            //A new IDocument was opened in the CoshhWindow
            else if (content.NewProperty != null)
            {
                foreach (IRibbonTabItem ribbonTab in content.NewProperty.RibbonTabs)
                {
                    View.Tabs.Add(ribbonTab.View);
                }
                content.NewProperty.RibbonTabs.CollectionChanged += contentRibbonTabsChanged;
                RibbonVisibility = true;
            }
        }

        //TODO: Move/Replace collection items logic
        private void contentRibbonTabsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (IRibbonTabItem ribbonTab in e.NewItems)
                    {
                        View.Tabs.Add(ribbonTab.View); 
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (IRibbonTabItem ribbonTab in e.OldItems)
                    {
                        Debug.Assert(View.Tabs.Contains(ribbonTab.View), "WARNING: The IWindows ribbon does not contain a tab the IDocument tried to remove.");
                        View.Tabs.Remove(ribbonTab.View);
                    }
                    break;
                case NotifyCollectionChangedAction.Reset:
                    View.Tabs.Clear();
                    break;
            }
            Debug.Assert(View.Tabs.Count == window.Content.RibbonTabs.Count, "WARNING: There's a difference between the ribbon tabs in the window vs the document");
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
