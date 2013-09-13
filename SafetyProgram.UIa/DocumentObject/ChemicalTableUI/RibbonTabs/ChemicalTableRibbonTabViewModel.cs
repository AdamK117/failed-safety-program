using System.Collections.ObjectModel;
using System.ComponentModel;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject.ChemicalTableUI
{
    /// <summary>
    /// Defines a standard implementation of an IChemicalTableRibbonViewModel.
    /// </summary>
    internal sealed class ChemicalTableRibbonTabViewModel : IChemicalTableRibbonViewModel
    {
        private readonly IApplicationConfiguration appConfiguration;

        public ChemicalTableRibbonTabViewModel(IApplicationConfiguration appConfiguration,
            IChemicalTableCommands commands)
        {
            Helpers.NullCheck(appConfiguration, commands);

            this.appConfiguration = appConfiguration;
            this.commands = commands;

            //Perform an initial blank search
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

        private readonly IChemicalTableCommands commands;

        /// <summary>
        /// Get a group of commands that act on the chemical table.
        /// </summary>
        public IChemicalTableCommands Commands
        {
            get { return commands; }
        }


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
