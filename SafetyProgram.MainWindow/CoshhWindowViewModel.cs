using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.MainWindow
{
    /// <summary>
    /// Defines a default ViewModel for the CoshhWindowView.
    /// </summary>
    public sealed class CoshhWindowViewModel : ICoshhWindowViewModel
    {
        /// <summary>
        /// Construct a new instance of the CoshhWindowViewModel.
        /// </summary>
        /// <param name="ribbonViewHolder">A holder containing the view for the applications top bar; specifically, the ribbon UI bar.</param>
        /// <param name="contentViewHolder">A holder containing the windows content.</param>
        /// <param name="hotkeys">A list of input bindings applicable to the window.</param>
        public CoshhWindowViewModel(IHolder<Ribbon> ribbonViewHolder,
            IHolder<Control> contentViewHolder,
            List<InputBinding> hotkeys)
        {
            Helpers.NullCheck(ribbonViewHolder, contentViewHolder, hotkeys);

            //Assignments.
            this.ribbonViewHolder = ribbonViewHolder;
            this.controlViewHolder = contentViewHolder;
            this.hotkeys = hotkeys;

            //Monitor if the ribbon view changes.
            this.ribbonViewHolder.ContentChanged += 
                (sender, newRibbon) => PropertyChanged.Raise(this, "RibbonView");

            //Monitor if the content (document) changes.
            this.controlViewHolder.ContentChanged += 
                (sender, newContent) => PropertyChanged.Raise(this, "ContentView");
        }

        private readonly IHolder<Ribbon> ribbonViewHolder;
        /// <summary>
        /// Gets the Ribbon view control associated with this CoshhWindowViewModel.
        /// </summary>
        public Ribbon RibbonView
        {
            get { return ribbonViewHolder.Content; }
        }

        private readonly IHolder<Control> controlViewHolder;
        /// <summary>
        /// Get the content view associated with the CoshhWindowViewModel.
        /// </summary>
        public Control ContentView
        {
            get { return controlViewHolder.Content; }
        }

        private readonly List<InputBinding> hotkeys;
        /// <summary>
        /// Gets the hotkeys associated with this CoshhWindowViewModel.
        /// </summary>
        public List<InputBinding> Hotkeys
        {
            get { return hotkeys; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
