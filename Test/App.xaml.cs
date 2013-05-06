using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using SafetyProgram.ModelObjects;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Configuration;
using SafetyProgram.Static;
using SafetyProgram.RepositoryIO;
using Microsoft.FSharp.Core;
using System.Xml.Linq;

namespace Test
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            IService<IConfiguration> configSvc = new LocalConfigurationFile(TestData.ConfigFile);
            IConfiguration config = configSvc.Load();

            var c = ConfigFileGetter.RetrieveO<IChemicalModelObject>(
                config,
                FuncConvert.ToFSharpFunc<Unit,IChemicalModelObject>(someUnit => new ChemicalModelObject())
            );
        
        }
    }
}
