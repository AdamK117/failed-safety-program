using System.Collections.ObjectModel;
using System.Windows;
using SafetyProgram.ModelObjects;
using SafetyProgram.UserControls.TagList;
using SafetyProgram.UserControls.Generic.EnhancedTagList;
using SafetyProgram.UserControls.Tooltips;
using SafetyProgram.UserControls.Tooltips.Fancy;

namespace SafetyProgram.UserControls
{
    /// <summary>
    /// Interaction logic for TestWindow.xaml
    /// </summary>
    public partial class TestWindow : Window
    {
        public TestWindow()
        {
            DataContext = this;

            SomeStrings = new ObservableCollection<object>()
            {
                new HazardModelObject() { Hazard = "Flammable" },
                new HazardModelObject() { Hazard = "Toxic" }, 
                new HazardModelObject() { Hazard = "Corrosive" },
                new HazardModelObject() { Hazard = "Poison" }
            };

            var someStrs = new ObservableCollection<HazardModelObject>()
            {
                new HazardModelObject() { Hazard = "Flammable" },
                new HazardModelObject() { Hazard = "Toxic" }, 
                new HazardModelObject() { Hazard = "Corrosive" },
                new HazardModelObject() { Hazard = "Poison" }
            };

            ConsBoxList = new EnhancedTagList<HazardModelObject>(someStrs,
                (HazardModelObject hazard) =>
                    {
                        return new TagListItem(
                            hazard.Hazard, 
                            () => someStrs.Remove(hazard),
                            new FancyToolTip(hazard.Hazard, hazard.Symbol, hazard.SignalWord)
                        );
                    }
                );

            InitializeComponent();
        }

        public ObservableCollection<object> SomeStrings
        {
            get;
            set;
        }

        public EnhancedTagList<HazardModelObject> ConsBoxList { get; private set; }
    }
}
