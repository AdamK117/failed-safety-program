using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Modularity;

//Internal dependancies
using SafetyProgram.RibbonView;
using SafetyProgram.MainWindow;
using SafetyProgram.Data;
using SafetyProgram.ICommands;


namespace PrismAggregatorDesignTest
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return new Shell();
        }

        protected override Microsoft.Practices.Prism.Modularity.IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();

            //Workers (Services etc.)
            catalog.AddModule(typeof(DataIModule));
            catalog.AddModule(typeof(ICommandsIModule));

            //Views
            catalog.AddModule(typeof(MainWindowIModule));
            catalog.AddModule(typeof(RibbonViewIModule));

            return catalog;
        }
    }
}
