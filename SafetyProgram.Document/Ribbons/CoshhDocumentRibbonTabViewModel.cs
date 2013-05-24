using System;
using SafetyProgram.Document.Commands;

namespace SafetyProgram.Document.Ribbons
{
    public sealed class CoshhDocumentRibbonTabViewModel : ICoshhDocumentRibbonTabViewModel
    {
        public CoshhDocumentRibbonTabViewModel(IDocumentICommands commands)
        {
            if (commands == null) throw new ArgumentNullException();
            else this.commands = commands;
        }

        private readonly IDocumentICommands commands;
        public Commands.IDocumentICommands Commands
        {
            get { return commands; }
        }
    }
}
