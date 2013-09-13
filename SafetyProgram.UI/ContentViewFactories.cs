using System;
using System.Windows.Controls;
using SafetyProgram.Base;
using SafetyProgram.Core.Commands.SelectionLogic;
using SafetyProgram.Core.Models;
using SafetyProgram.UI.ModelViews.ContentViews.DocumentObjects.ChemicalTables.Default;

namespace SafetyProgram.UI
{
    internal static class ContentViewFactories
    {
        public static Func<IDocumentObject, Control> DocumentObjectViewFactory(
            IApplicationConfiguration configuration,
            ICommandInvoker commandInvoker,
            ISelectionManager selectionManager)
        {
            Helpers.NullCheck(configuration,
                commandInvoker,
                selectionManager);

            return
                (model) =>
                {
                    switch (model.Identifier)
                    {
                        case ModelIdentifiers.CHEMICAL_TABLE_IDENTIFIER:
                            return new ChemicalTableView(
                                new ChemicalTableViewModel(
                                    model as IChemicalTable));

                        default:
                            throw new NotImplementedException();
                    }
                };
        }
    }
}
