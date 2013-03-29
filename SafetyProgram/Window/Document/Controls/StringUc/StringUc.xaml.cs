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

namespace SafetyProgram.MainWindow.Document.Controls.StringUc
{
    /// <summary>
    /// Interaction logic for StringUc.xaml
    /// </summary>
    public partial class StringUc : UserControl
    {
        public StringUc()
        {
            InitializeComponent();
        }

        public StringUc(string myString)
        {
            LayoutRoot.Text = myString;
            InitializeComponent();            
        }
    }
}
