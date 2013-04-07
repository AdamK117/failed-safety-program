namespace SafetyProgram.MainWindow.Document.Commands
{
    public abstract class DocumentCommandsBase : BaseICommand
    {
        protected readonly CoshhDocument document;

        public DocumentCommandsBase(CoshhDocument document)
        {
            this.document = document;
        }
    }
}
