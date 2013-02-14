using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

namespace SafetyProgram.MainWindow
{
    public class MainWindowIModule : IModule
    {
        private IRegionManager regionManager;

        public MainWindowIModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void Initialize()
        {
            regionManager.RegisterViewWithRegion("MainWindow", typeof(MainWindowView));
        }
    }
}
