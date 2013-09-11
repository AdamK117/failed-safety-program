using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows;
using Fluent;
using SafetyProgram.Base;

namespace SafetyProgram.UI
{
    public sealed partial class RibbonView : Ribbon
    {
        private readonly IRibbonViewModel viewModel;

        public RibbonView(IRibbonViewModel viewModel)
        {
            Helpers.NullCheck(viewModel);

            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
