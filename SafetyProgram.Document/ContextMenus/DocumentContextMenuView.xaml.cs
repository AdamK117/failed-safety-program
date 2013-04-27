﻿using System.Windows.Controls;

namespace SafetyProgram.Document.ContextMenus
{
    /// <summary>
    /// Interaction logic for DocumentContextMenuView.xaml
    /// </summary>
    internal sealed partial class DocumentContextMenuView : ContextMenu
    {
        private readonly DocumentContextMenu viewModel;

        public DocumentContextMenuView(DocumentContextMenu viewModel)
        {
            this.viewModel = viewModel;

            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
