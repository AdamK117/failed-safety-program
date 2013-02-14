using SafetyProgram.Models.DataModels;

namespace SafetyProgram.MainWindow
{
    public class ProcessViewModel : BaseViewModel
    {
        private new ProcessModel model;

        public ProcessViewModel(ProcessModel model) : base(model)
        {
            this.model = model;
        }

        public ProcessModel Model { get { return model; } }
    }
}
