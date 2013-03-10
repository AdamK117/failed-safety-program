using Microsoft.Practices.Unity;
using Microsoft.Practices.Prism.Modularity;

namespace SafetyProgram.Data
{
    [Module(ModuleName = "DataIModule")]
    public class DataIModule : IModule
    {
        private IUnityContainer container;

        public DataIModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            //container.RegisterType<CurrentlyOpen>(new ContainerControlledLifetimeManager());
        }
    }
}
