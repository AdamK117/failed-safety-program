using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Configuration;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;
using SafetyProgram.ModelObjects;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon
{
    internal sealed class ChemicalTableRibbonTab : IChemicalTableRibbonTab
    {
        public ChemicalTableRibbonTab(
            IChemicalTable table,
            Func<IChemicalTableRibbonTab, RibbonTabItem> viewConstructor
            )
        {
            if (table == null ||
                viewConstructor == null)
            {
                throw new ArgumentNullException(); 
            }
            else
            {
                this.table = table;

                ////Create a service to load a repository
                //var repositoryService = new LocalFileService<IRepository<IChemicalModelObject>>(
                //    new RepositoryLocalFileFactory<IChemicalModelObject>(
                //        new ChemicalModelObjectLocalFileFactory()
                //        ),
                //        "V:\\SafetyProgram\\SafetyProgram.TestData\\ChemicalRepository.xml"
                //    );

                ////Load it
                //var repository = repositoryService.Load();

                //var unsortedChemicals = new List<IChemicalModelObject>(repository.Entries);
                //unsortedChemicals.Sort(
                //    (chem1, chem2) => String.Compare(chem1.Name, chem2.Name)
                //    );
                //allLoadedChemicals = unsortedChemicals;

                Chemicals = new ObservableCollection<IChemicalModelObject>(allLoadedChemicals);

                view = viewConstructor(this);
            }
        }

        private readonly IChemicalTable table;
        public IChemicalTableCommands Commands
        {
            get
            {
                return table.Commands;
            }
        }

        private IEnumerable<IChemicalModelObject> allLoadedChemicals;
        public ObservableCollection<IChemicalModelObject> Chemicals
        {
            get;
            private set;
        }

        private string currentSearch;
        public string Search
        {
            get
            {
                return currentSearch;
            }
            set
            {
                currentSearch = value;
                filterChemicals(currentSearch);
            }
        }

        private void filterChemicals(string filterString)
        {
            if (filterString != "")
            {
            }
            else
            {
                var unsortedChemicals = new List<IChemicalModelObject>(Chemicals);
                foreach (var chemical in allLoadedChemicals)
                {
                    if (!Chemicals.Contains(chemical))
                    {
                        unsortedChemicals.Add(chemical);
                        Chemicals.Add(chemical);
                    }
                }
            }
            Chemicals.Clear();

            var filteredChemicals = allLoadedChemicals.Where(
                (chemicalEntry) =>
                {
                    return chemicalEntry
                        .Name
                        .ToLower()
                        .Contains(currentSearch);
                }
            );

            foreach (IChemicalModelObject filteredChemical in filteredChemicals)
            {
                Chemicals.Add(filteredChemical);
            }
        }

        private void unfilterChemicals()
        {
            Chemicals.Clear();
            foreach (IChemicalModelObject chemical in allLoadedChemicals)
            {
                Chemicals.Add(chemical);
            }
        }

        private readonly RibbonTabItem view;
        public RibbonTabItem View 
        { 
            get 
            { 
                return view; 
            } 
        }
        Control IViewable.View 
        {
            get 
            { 
                return view; 
            } 
        }
    }
}
