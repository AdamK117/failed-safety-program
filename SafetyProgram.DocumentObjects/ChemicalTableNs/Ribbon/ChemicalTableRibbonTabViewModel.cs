using System.Collections.ObjectModel;
using System.ComponentModel;
using SafetyProgram.Base;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjects.ChemicalTableNs.Commands;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon
{
    internal sealed class ChemicalTableRibbonTabViewModel : IChemicalTableRibbonViewModel
    {
        private readonly IConfiguration appConfiguration;

        public ChemicalTableRibbonTabViewModel(IConfiguration appConfiguration,
            IChemicalTableCommands commands)
        {
            Helpers.NullCheck(appConfiguration, commands);

            this.appConfiguration = appConfiguration;
            this.commands = commands;

            //Perform an initial blank search
            performSearch("");
        }

        private string search = "";
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

        private readonly ObservableCollection<IChemicalModelObject> searchResult = new ObservableCollection<IChemicalModelObject>();
        public ObservableCollection<IChemicalModelObject> SearchResult
        {
            get { return searchResult; }
        }

        private readonly IChemicalTableCommands commands;
        public IChemicalTableCommands Commands
        {
            get { return commands; }
        }

        private void performSearch(string searchString)
        {
            searchResult.Clear();

            if (searchString == "")
            {
                foreach (IRepository<IChemicalModelObject> repository in appConfiguration.ChemicalRepositories)
                {
                    repository.GetAll(
                        ((retrievedChemical) => searchResult.Add(retrievedChemical))
                    );
                }
            }
            else
            {
                foreach (IRepository<IChemicalModelObject> repository in appConfiguration.ChemicalRepositories)
                {
                    repository.Get(
                        ((someChemical) =>
                            someChemical.Name.ToLower().Contains(searchString.ToLower())
                        ),
                        (foundChemical) => searchResult.Add(foundChemical)
                    );
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
