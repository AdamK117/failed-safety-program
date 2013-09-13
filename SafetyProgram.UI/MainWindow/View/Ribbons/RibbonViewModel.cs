using System.Collections.ObjectModel;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands.ICommands;

namespace SafetyProgram.UI.MainWindow
{
    internal sealed class RibbonViewModel : 
        IRibbonViewModel
    {
        public RibbonViewModel(ICoreCommands coreCommands,
            ObservableCollection<RibbonTabItem> ribbonTabs)
        {
            Helpers.NullCheck(coreCommands, ribbonTabs);
            
            this.Commands = coreCommands;
            this.RibbonTabs = ribbonTabs;
        }

        /// <summary>
        /// Get the ribbon tabs for the ribbon.
        /// </summary>
        public ObservableCollection<
            RibbonTabItem> RibbonTabs { get; private set; }

        /// <summary>
        /// Get the commands available to the ribbon.
        /// </summary>
        public ICoreCommands Commands { get; private set; }
    }
}
