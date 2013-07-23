using System;
using System.Windows.Controls;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Models;

namespace SafetyProgram.DocumentObjectUi
{
    public sealed class DocumentObjectAbstractFactory : IOutputFactory<Control, IDocumentObject>
    {
        public DocumentObjectAbstractFactory(IConfiguration configuration,
            ICommandInvoker commandInvoker)
        { }

        public Control Load(IDocumentObject data)
        {
            throw new NotImplementedException();
        }
    }
}
