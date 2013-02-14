using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Prism.UnityExtensions;
using Microsoft.Practices.Prism.Modularity;

namespace SafetyProgram.UserControls.CoshhChemical
{
    class Bootstrapper : UnityBootstrapper
    {
        protected override System.Windows.DependencyObject CreateShell()
        {
            return new EditCoshhChemical();
        }

        protected override Microsoft.Practices.Prism.Modularity.IModuleCatalog CreateModuleCatalog()
        {
            ModuleCatalog catalog = new ModuleCatalog();
            return catalog;
        }
    }
}
