using System.Windows;
using System.Windows.Controls;
using SafetyProgram.Base.Interfaces;

namespace SafetyProgram.DocumentObjects.ChemicalTableNs.Ribbon
{
    /// <summary>
    /// Interaction logic for ChemicalMenuItem.xaml
    /// </summary>
    public partial class ChemicalMenuItem : UserControl
    {
        public ChemicalMenuItem()
        {
            InitializeComponent();
        }

        public static readonly DependencyProperty ChemicalProperty = DependencyProperty.Register
            (
                "Chemical",
                typeof(IChemicalModelObject),
                typeof(ChemicalMenuItem),
                new FrameworkPropertyMetadata
                        (
                            null,
                            FrameworkPropertyMetadataOptions.BindsTwoWayByDefault
                        )              
            );

        public IChemicalModelObject Chemical
        {
            get;
            private set;
        }
    }
}
