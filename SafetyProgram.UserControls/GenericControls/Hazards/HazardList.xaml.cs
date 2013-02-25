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
using SafetyProgram.Models.DataModels;

namespace SafetyProgram.UserControls.Controls.Hazards
{
    /// <summary>
    /// Interaction logic for HazardList.xaml
    /// </summary>
    public partial class HazardList : UserControl
    {
        public HazardList()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public static readonly DependencyProperty HazardsProperty = DependencyProperty.Register
            (
                "Hazards",
                typeof(IList<HazardModel>),
                typeof(HazardList),
                new FrameworkPropertyMetadata
                    (
                        null,
                        FrameworkPropertyMetadataOptions.None,
                        new PropertyChangedCallback
                        (
                            hazardsChanged
                        )
                    )
            );


        static void hazardsChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            removeHazardICommand = new RemoveHazardICommand(e.NewValue as IList<HazardModel>);
        }

        public IList<HazardModel> Hazards
        {
            get { return (IList<HazardModel>)GetValue(HazardsProperty); }
            set { SetValue(HazardsProperty, value); }
        }

        private static RemoveHazardICommand removeHazardICommand;
        public RemoveHazardICommand RemoveHazardICommand
        {
            get { return removeHazardICommand; }
        }
    }
}
