using Fluent;
using System.Windows;
using System;
using Microsoft.Practices.ServiceLocation;

namespace PrismAggregatorDesignTest
{
    /// <summary>
    /// Interaction logic for Shellz.xaml
    /// </summary>
    public partial class Shell : RibbonWindow
    {
        public Shell()
        {
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            Application.Current.Shutdown();
        }
    }
}
