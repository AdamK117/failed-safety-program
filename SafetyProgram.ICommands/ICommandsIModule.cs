using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Unity;

namespace SafetyProgram.ICommands
{
    [Module(ModuleName = "ICommandsIModule")]
    [ModuleDependency("DataIModule")]
    public class ICommandsIModule: IModule
    {
        private IUnityContainer container;

        public ICommandsIModule(IUnityContainer container)
        {
            this.container = container;
        }

        public void Initialize()
        {
            RegisterServices();
        }

        public void RegisterServices()
        {
            container.RegisterType<ICommandsHolder>(new ContainerControlledLifetimeManager());
            container.RegisterType<HotkeysHolder>(new ContainerControlledLifetimeManager());
        }
    }
}
