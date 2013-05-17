using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using SafetyProgram.Base.DocumentFormats;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.Document.Body;
using SafetyProgram.Document.Commands;
using SafetyProgram.Document.ContextMenus;
using SafetyProgram.Document.Ribbons;

namespace SafetyProgram.Document
{
    public static class CoshhDocumentDefault
    {
        public static CoshhDocument DefaultCoshhDocument(IConfiguration appConfiguration, ICommandInvoker commandInvoker)
        {
            return new CoshhDocument(
                appConfiguration,
                DefaultTitle,
                DefaultFormat(),
                DefaultBody(),
                CommandsConstructor(commandInvoker),
                ContextMenuConstructor,
                RibbonTabsConstructor,            
                ViewConstructor
                );
        }

        public static Func<ICoshhDocument, IDocumentICommands> CommandsConstructor(ICommandInvoker commandInvoker)
        {
            return (coshhDoc) =>
                {
                    return new DocumentICommands(coshhDoc, commandInvoker);
                };
        }

        public static IContextMenu ContextMenuConstructor(IDocumentICommands coshhDocument)
        {
            return new DocumentContextMenu(
                coshhDocument,
                (contextMenuVm) => new DocumentContextMenuView(contextMenuVm)
                );
        }

        public static ObservableCollection<IRibbonTabItem> RibbonTabsConstructor(ICoshhDocument coshhDocument)
        {
            var ribbonTabItems = new ObservableCollection<IRibbonTabItem>();
            ribbonTabItems.Add(
                new CoshhDocumentRibbonTab(
                    coshhDocument,
                    (ribbonTabVm) => new CoshhDocumentRibbonTabView(ribbonTabVm)
                    )
                );
            return ribbonTabItems;
        }

        public const string DefaultTitle = "someDefaultTitle";

        public static IFormat DefaultFormat()
        {
            return new A4Format();
        }

        public static IDocumentBody DefaultBody()
        {
            return new CoshhDocumentBody();
        }

        public static Control ViewConstructor(ICoshhDocument coshhDocument)
        {
            return new CoshhDocumentView(coshhDocument);
        }
    }
}
