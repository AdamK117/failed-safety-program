﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Core;

namespace SafetyProgram.UI.DocumentObject
{
    public sealed class DocumentObjectUiControllerFactory 
        : IDocumentObjectUiControllerFactory
    {
        private readonly IConfiguration configuration;
        private readonly ICommandInvoker commandInvoker;

        public DocumentObjectUiControllerFactory(IConfiguration configuration, 
            ICommandInvoker commandInvoker)
        {
            this.configuration = configuration;
            this.commandInvoker = commandInvoker;
        }

        public Core.Models.IDocumentObject GetDocumentObject(IDocumentObjectUiController controller)
        {
            throw new NotImplementedException();
        }

        public IDocumentObjectUiController GetDocumentObjectUiController(Core.Models.IDocumentObject documentObject)
        {
            throw new NotImplementedException();
        }
    }
}
