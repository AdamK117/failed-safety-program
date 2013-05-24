using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Input;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    public sealed class ChemicalTableViewModel : IChemicalTableViewModel
    {
        public ChemicalTableViewModel(IEditableHolder<string> headerHolder,
            ContextMenu contextMenu,
            ObservableCollection<ICoshhChemicalObject> chemicals,
            ObservableCollection<ICoshhChemicalObject> selectedChemicals,
            List<InputBinding> hotkeys,
            Action selectAction)
        {
            if (headerHolder == null ||
                contextMenu == null ||
                chemicals == null ||
                selectedChemicals == null ||
                hotkeys == null ||
                selectAction == null)
                throw new ArgumentNullException();
            else
            {
                this.headerHolder = headerHolder;
                this.contextMenu = contextMenu;
                this.chemicals = chemicals;
                this.selectedChemicals = selectedChemicals;
                this.hotkeys = hotkeys;
                this.selectAction = selectAction;

                this.headerHolder.ContentChanged += (sender, newHeader) => PropertyChanged.Raise(this, "Header");
            }
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

        private readonly ObservableCollection<ICoshhChemicalObject> chemicals;
        public ObservableCollection<ICoshhChemicalObject> Chemicals
        {
            get { return chemicals; }
        }

        private readonly ObservableCollection<ICoshhChemicalObject> selectedChemicals;
        public ObservableCollection<ICoshhChemicalObject> SelectedChemicals
        {
            get { return selectedChemicals; }
        }

        private readonly List<InputBinding> hotkeys;
        public List<InputBinding> Hotkeys
        {
            get { return hotkeys; }
        }

        private readonly Action selectAction;
        public void Select()
        {
            selectAction();
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
