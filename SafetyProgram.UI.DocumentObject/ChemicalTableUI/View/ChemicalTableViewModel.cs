using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    internal sealed class ChemicalTableViewModel : IChemicalTableViewModel
    {
        public ChemicalTableViewModel(IEditableHolder<string> headerHolder,
            ContextMenu contextMenu,
            ObservableCollection<ICoshhChemical> chemicals,
            ObservableCollection<ICoshhChemical> selectedChemicals,
            List<InputBinding> hotkeys)
        {
            Helpers.NullCheck(headerHolder,
                contextMenu,
                chemicals,
                selectedChemicals);

            this.headerHolder = headerHolder;
            this.contextMenu = contextMenu;
            this.chemicals = chemicals;
            this.selectedChemicals = selectedChemicals;
            this.hotkeys = hotkeys;

            this.headerHolder.ContentChanged += (sender, newHeader) => PropertyChanged.Raise(this, "Header");
        }

        private readonly IEditableHolder<string> headerHolder;
        public string Header
        {
            get { return headerHolder.Content; }
            set
            {
                headerHolder.Content = value;
            }
        }

        private readonly ContextMenu contextMenu;
        public ContextMenu ContextMenu
        {
            get { return contextMenu; }
        }

        private readonly ObservableCollection<ICoshhChemical> chemicals;
        public ObservableCollection<ICoshhChemical> Chemicals
        {
            get { return chemicals; }
        }

        private readonly ObservableCollection<ICoshhChemical> selectedChemicals;
        public ObservableCollection<ICoshhChemical> SelectedChemicals
        {
            get { return selectedChemicals; }
        }

        private readonly List<InputBinding> hotkeys;
        public List<InputBinding> Hotkeys
        {
            get { return hotkeys; }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
