using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs
{
    internal sealed class ChemicalTable : IChemicalTable
    {
        public ChemicalTable(IHolder<string> headerHolder,
            ObservableCollection<ICoshhChemicalObject> chemicals,
            RibbonTabItem contextualRibbonTab,
            Control view)
        {
            Helpers.NullCheck(headerHolder, chemicals, contextualRibbonTab, view);

            this.headerHolder = headerHolder;
            this.chemicals = chemicals;
            this.contextualRibbonTab = contextualRibbonTab;
            this.view = view;

            chemicals.CollectionChanged += (sender, e) => FlagAsEdited();
        }

        private readonly IHolder<string> headerHolder;
        public string Header
        {
            get
            {
                return headerHolder.Content;
            }
        }

        private readonly ObservableCollection<ICoshhChemicalObject> chemicals;
        public ObservableCollection<ICoshhChemicalObject> Chemicals
        {
            get { return chemicals; }
        }

        private readonly RibbonTabItem contextualRibbonTab;
        public RibbonTabItem ContextualTab
        {
            get { return contextualRibbonTab; }
        }

        private readonly Control view;
        public Control View
        {
            get { return view; }
        }

        public void FlagAsEdited()
        {
            if (EditedFlag == false)
            {
                editedFlag = true;
                EditedFlagChanged.Raise(this, editedFlag);
            }
        }

        private bool editedFlag;
        public bool EditedFlag
        {
            get { return editedFlag; }
        }

        public event EventHandler<GenericPropertyChangedEventArg<bool>> EditedFlagChanged;
    }
}
