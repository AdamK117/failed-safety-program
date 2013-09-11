using System;
using System.Collections.Generic;
using System.IO;
using SafetyProgram.Base;
using SafetyProgram.Core.Models;

namespace SafetyProgram.UI.DocumentObject
{
    /// <summary>
    /// Defines a standard implementation of the IDocumentObjectUiControllerFactory 
    /// abstract factory.
    /// </summary>
    public sealed class DocumentObjectUiControllerFactory 
        : IDocumentObjectUiControllerFactory
    {
        private readonly IApplicationConfiguration configuration;
        private readonly ICommandInvoker commandInvoker;

        /// <summary>
        /// Construct an instance of a <code>DocumentObjectUiControllerFactory</code>,
        /// an abstract factory for generating UI controllers from models.
        /// </summary>
        /// <param name="applicationConfiguration">The application configuration that shall be passed into
        /// instances created by the factory.</param>
        /// <param name="commandInvoker">The command invoker that shall be passed into instances created by
        /// the factory.</param>
        public DocumentObjectUiControllerFactory(IApplicationConfiguration applicationConfiguration, 
            ICommandInvoker commandInvoker)
        {
            Helpers.NullCheck(applicationConfiguration, commandInvoker);

            this.configuration = applicationConfiguration;
            this.commandInvoker = commandInvoker;

            lookupDictionary = new Dictionary<Type, Func<IDocumentObject, IDocumentObjectUiController>>();
            /*{
                {   typeof(ChemicalTable),
                    (documentObject) => 
                        new ChemicalTableViewController(documentObject as IChemicalTable, configuration, commandInvoker)}
            };*/
        }

        private readonly IDictionary<
            Type, 
            Func<IDocumentObject, IDocumentObjectUiController>> lookupDictionary;

        /// <summary>
        /// Create an <code>IDocumentUiController</code> using the supplied <code>IDocumentObject</code>
        /// model.
        /// </summary>
        /// <param name="documentObject"></param>
        /// <returns></returns>
        public IDocumentObjectUiController GetDocumentObjectUiController(IDocumentObject documentObject)
        {
            var typeArg = documentObject.GetType();

            if (lookupDictionary.ContainsKey(typeArg))
            {
                return lookupDictionary[typeArg](documentObject);
            }
            else throw new InvalidDataException();
        }
    }
}
