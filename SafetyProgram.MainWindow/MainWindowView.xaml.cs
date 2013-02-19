﻿using System.Windows.Controls;
using SafetyProgram.Data.CoshhFile;
using Microsoft.Practices.ServiceLocation;
using SafetyProgram.Data;
using SafetyProgram.UserControls;

namespace SafetyProgram.MainWindow
{
    /// <summary>
    /// Interaction logic for MainWindowView.xaml
    /// </summary>
    public partial class MainWindowView : UserControl
    {
        private CurrentlyOpen currentlyOpen;
        public MainWindowView(MainWindowViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
            currentlyOpen = ServiceLocator.Current.GetInstance<CurrentlyOpen>();
            currentlyOpen.Data.DocObject.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(DocObject_CollectionChanged);

            foreach (IDocUserControl doc in currentlyOpen.Data.DocObject)
            {
                LayoutRoot.Children.Add(doc.Display());
            }
        }

        void DocObject_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            foreach (IDocUserControl doc in currentlyOpen.Data.DocObject)
            {
                LayoutRoot.Children.Add(doc.Display());
            }
        }
    }
}
