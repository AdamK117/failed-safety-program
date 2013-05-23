using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ninject;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.MainWindow
{
    internal sealed class ap
    {
        public ap()
        {
            using (IKernel kernel = new StandardKernel())
            {
                //Viewmodel
                kernel
                    .Bind<IMainWindowViewModel>()
                    .To<MainWindowViewModel>();

                //Default Document
                kernel
                    .Bind<IDocument>()
                    .To<MockDocument>();


            }
        }
    }
}
