using System.Collections.ObjectModel;
using System.ComponentModel;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands.ICommands;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.Views.ModelViews.RibbonTabViews.DocumentObjects.ChemicalTables
{
    /// <summary>
    /// Defines a standard implementation of an IChemicalTableRibbonViewModel.
    /// </summary>
    public sealed class ChemicalTableRibbonTabViewModel : IChemicalTableRibbonViewModel
    {
        private readonly IApplicationConfiguration appConfiguration;

        public ChemicalTableRibbonTabViewModel(IChemicalTable model,
            IApplicationConfiguration applicationConfiguration,
            ICommandInvoker commandInvoker,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(model,
                appConfiguration,
                commandInvoker,
                selectionManager);

            this.Commands = new ChemicalTableICommands(
                model,
                commandInvoker);

            this.appConfiguration = applicationConfiguration;

            performSearch("");
        }

        private string search = "";

        /// <summary>
        /// Get or set the current search phrase.
        /// </summary>
        public string Search
        {
            get
            {
                return search;
            }
            set
            {
                search = value;
                PropertyChanged.Raise(this, "Search");
                performSearch(search);
            }
        }

        private readonly ObservableCollection<IChemical> searchResult 
            = new ObservableCollection<IChemical>();

        /// <summary>
        /// Get the search result yielded from the search phrase.
        /// </summary>
        public ObservableCollection<IChemical> SearchResult
        {
            get { return searchResult; }
        }

        /// <summary>
        /// Get a group of commands that act on the chemical table.
        /// </summary>
        public IChemicalTableCommands Commands { get; private set; }


        /// <summary>
        /// Performs a search for chemicals using the string.
        /// </summary>
        /// <param name="searchString"></param>
        private void performSearch(string searchString)
        {
            searchResult.Clear();

            if (searchString == "")
            {
                //foreach (IRepository<IChemicalModelObject> repository in appConfiguration.ChemicalRepositories)
                //{
                //    repository.GetAll(
                //        ((retrievedChemical) => searchResult.Add(retrievedChemical))
                //    );
                //}
            }
            else
            {
                //foreach (IRepository<IChemicalModelObject> repository in appConfiguration.ChemicalRepositories)
                //{
                //    repository.Get(
                //        ((someChemical) =>
                //            someChemical.Name.ToLower().Contains(searchString.ToLower())
                //        ),
                //        (foundChemical) => searchResult.Add(foundChemical)
                //    );
                //}
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
