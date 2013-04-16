using SafetyProgram.BaseClasses;

namespace SafetyProgram.Document.Commands
{
    public abstract class DocumentCommandsBase : BaseICommand
    {
        protected readonly CoshhDocument document;

        /// <summary>
        /// Constructs a base ICommand that uses the CoshhDocument
        /// </summary>
        /// <param name="document"></param>
        public DocumentCommandsBase(CoshhDocument document)
        {
            this.document = document;
        }
    }
}
