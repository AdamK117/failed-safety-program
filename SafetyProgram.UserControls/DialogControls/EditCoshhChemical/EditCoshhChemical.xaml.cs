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
using System.Windows.Shapes;
using SafetyProgram.Models.DataModels;
using SafetyProgram.UserControls.Controls;
using SafetyProgram.UserControls.Controls.Hazards;

namespace SafetyProgram.UserControls.DialogControls
{
    /// <summary>
    /// Interaction logic for EditCoshhChemical.xaml
    /// </summary>
    public partial class EditCoshhChemical : Window
    {
        public EditCoshhChemical()
        {
            InitializeComponent();
            this.DataContext = new ViewModel();
        }

        public EditCoshhChemical(CoshhChemicalModel model)
        {
            InitializeComponent();
            this.DataContext = new ViewModel(model);
        }
    }
}
