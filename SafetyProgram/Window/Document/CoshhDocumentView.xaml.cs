using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using SafetyProgram.MainWindow.Document.Controls;
using System.Collections.ObjectModel;
using SafetyProgram.MainWindow;
using SafetyProgram.MainWindow.Document.Controls.StringUc;

namespace SafetyProgram.MainWindow.Document
{
    /// <summary>
    /// Interaction logic for CoshhDocumentView.xaml
    /// </summary>
    public partial class CoshhDocumentView : UserControl
    {
        private CoshhDocument viewModel;
        public CoshhDocumentView(CoshhDocument viewModel)
        {
            this.viewModel = viewModel;
            this.DataContext = viewModel;
            InitializeComponent();
        }
    }
}
