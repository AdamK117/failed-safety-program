using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Modularity;

namespace SafetyProgram.RibbonView
{
    public class RibbonViewIModule : IModule
    {
        private IRegionManager regionManager;

        public RibbonViewIModule(IRegionManager regionManager)
        {
            this.regionManager = regionManager;
        }
        public void Initialize()
        {
            this.regionManager.RegisterViewWithRegion("RibbonView", typeof(RibbonView));
        }
    }
}
