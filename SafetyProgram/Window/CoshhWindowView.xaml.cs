using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
//using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

using Fluent;

using SafetyProgram.MainWindow.IO;
using SafetyProgram.MainWindow.Document;
//using System.Windows.Forms;

namespace SafetyProgram.Window
{
    /// <summary>
    /// Interaction logic for CoshhWindow.xaml
    /// </summary>
    public partial class CoshhWindowView : RibbonWindow
    {
        /// <summary>
        /// Makes an instance of the CoshhWindow object.
        /// </summary>
        public CoshhWindowView(CoshhWindow viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            System.Windows.Application.Current.Shutdown();
        }
    }
}
