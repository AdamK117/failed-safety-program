using System;
using System.Collections.Generic;
using System.Windows.Controls;
using SafetyProgram.Base.Interfaces;
using SafetyProgram.DocumentObjectUi;
using SafetyProgram.DocumentUi.Commands;
using SafetyProgram.DocumentUi.ContextMenus;
using SafetyProgram.DocumentUi.Ribbons;
using SafetyProgram.Models;

namespace SafetyProgram.DocumentUi.View
{
    public sealed class DocumentViewModel : ICoshhDocumentViewModel
    {
        public DocumentViewModel(IDocument document,
            IConfiguration configuration,
            ICommandInvoker commandInvoker)
        {
            IDocumentICommands documentCommands = new DocumentICommands(document);

            // Make a document ribbon.
            var ribbon = new DocumentRibbonTabView(
                new DocumentRibbonTabViewModel(
                    documentCommands
                )
            );

            ContextMenu menu = new DocumentContextMenuView(
                new DocumentContextMenuViewModel(
                    documentCommands
                )
            );

            var factory = new DocumentObjectAbstractFactory(configuration, commandInvoker);
            foreach (IDocumentObject docObject in document.Items)
            {
                BodyItems.Add(factory.Load(docObject));
            }
        }

        public System.Windows.Controls.ContextMenu Menu
        {
            get { throw new NotImplementedException(); }
        }

        public List<System.Windows.Input.InputBinding> Hotkeys
        {
            get { throw new NotImplementedException(); }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;


        public System.Collections.ObjectModel.ObservableCollection<Control> BodyItems
        {
            get { throw new NotImplementedException(); }
        }

        public Models.IFormat Format
        {
            get { throw new NotImplementedException(); }
        }
    }
}
