using System;
using System.Collections.Generic;
using Fluent;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.ModelViews.RibbonTabViews.DocumentObjects.ChemicalTables;
using SafetyProgram.UI.ModelViews.RibbonTabViews.Documents;

namespace SafetyProgram.UI
{
    internal static class RibbonFactories
    {
        public static Func<IDocument, IEnumerable<RibbonTabItem>> DocumentRibbonTabFactory(
            ICommandInvoker commandInvoker)
        {
            Helpers.NullCheck(commandInvoker);

            return
                (model) =>
                {
                    return new List<RibbonTabItem>()
                    {
                        {
                            new InsertRibbonTabView(
                                new InsertRibbonTabViewModel(
                                    model,
                                    commandInvoker))
                        }
                    };
                };
        }

        public static Func<IDocumentObject, RibbonTabItem> DocumentObjectContextualRibbonTabFactory(
            IApplicationConfiguration applicationConfiguration,
            ICommandInvoker commandInvoker,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(applicationConfiguration,
                commandInvoker,
                selectionManager);

            return
                (model) =>
                {
                    switch (model.Identifier)
                    {
                        case ModelIdentifiers.CHEMICAL_TABLE_IDENTIFIER:
                            return new ChemicalTableRibbonView(
                                new ChemicalTableRibbonTabViewModel(
                                    model as IChemicalTable,
                                    applicationConfiguration,
                                    commandInvoker,
                                    selectionManager));
                        default:
                            throw new NotImplementedException();
                    }
                };
        }

    }
}
