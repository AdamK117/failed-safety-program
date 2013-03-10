using Microsoft.Practices.ServiceLocation;
using SafetyProgram.Data.DOM;

namespace SafetyProgram.MainWindow
{
    public class MainWindowViewModel : BaseINPC
    {
        private CoshhDocument currentlyOpen;

        public MainWindowViewModel()
        {     
            currentlyOpen = ServiceLocator.Current.GetInstance<CoshhDocument>();
            currentlyOpen.IsOpenChanged += new CoshhDocument.isOpenChangedDelegate(currentlyOpen_IsOpenChangedEvent);
        }

        void currentlyOpen_IsOpenChangedEvent(bool isOpen)
        {
            RaisePropertyChanged("IsOpen");
        }
        public string IsOpen
        {
            get { return currentlyOpen.IsOpen() ? "Visible" : "Hidden"; }
        }
    }
}
