namespace SafetyProgram.ICommands
{
    //Holds global, application-wide, commands.
    public class ICommandsHolder
    {
        private SaveICommand saveCommand = new SaveICommand();
        public SaveICommand SaveCommand { get { return saveCommand; } }

        private SaveAsICommand saveAsCommand = new SaveAsICommand();
        public SaveAsICommand SaveAsCommand { get { return saveAsCommand; } }

        private SaveAsPDFICommand saveAsPDFCommand = new SaveAsPDFICommand();
        public SaveAsPDFICommand SaveAsPDFCommand { get { return saveAsPDFCommand; } }

        private LoadFileICommand loadFileCommand = new LoadFileICommand();
        public LoadFileICommand LoadFileCommand { get { return loadFileCommand; } }

        private CloseICommand closeCommand = new CloseICommand();
        public CloseICommand CloseCommand { get { return closeCommand; } }

        private NewFileICommand newFileCommand = new NewFileICommand();
        public NewFileICommand NewFileCommand { get { return newFileCommand; } }

        private DeleteSelectedICommand deleteSelectedCommand = new DeleteSelectedICommand();
        public DeleteSelectedICommand DeleteSelectedCommand { get { return deleteSelectedCommand; } }

        private EditChemicalICommand editChemicalCommand = new EditChemicalICommand();
        public EditChemicalICommand EditChemicalCommand { get { return editChemicalCommand; } }
    }
}
