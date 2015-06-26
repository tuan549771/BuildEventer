//! All rights reserved
//!

using PostBuildCopy.Classes;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    /// <summary>
    /// Interaction logic for DestinationListBox.xaml
    /// </summary>
    public partial class DestinationListBox : UserControl
    {
        #region Constructors
        public DestinationListBox()
        {
            InitializeComponent();
        }
        #endregion

        #region Event handlers

        private void DropDestination(object sender, DragEventArgs e)
        {
            object path = e.Data.GetData(typeof(string));
            if (path != null)
            {
                string pathDest = path.ToString();
                if (File.Exists(pathDest))
                {
                    MessageBox.Show("You choice destination is a folder, Please!","My App", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }
                pathDest = subFunctions.GetPathRoot(pathDest);
                if (null == pathDest)
                    return;
                ObservableCollection<Destination> destinationTemps = new ObservableCollection<Destination>();
                ObservableCollection<Source> sourcesTemps = new ObservableCollection<Source>();
                CopySourcesToDestination copySourcesToDest = new CopySourcesToDestination();

                destinationTemps.Add(new Destination() { destination = pathDest });
                copySourcesToDest.destination = destinationTemps[destinationTemps.Count - 1];
                copySourcesToDest.sources = sourcesTemps;

                VariablesManager.actionManagers.listAction.Add(copySourcesToDest);

                lbDest.ItemsSource = subFunctions.GetDestFromActionManager(VariablesManager.actionManagers);
                lbDest.SelectedIndex = lbDest.Items.Count - 1;
            }
        }

        private void lbSelectionChangedDest(object sender, SelectionChangedEventArgs e)
        {
            VariablesManager.indexDest = lbDest.SelectedIndex;
            LoadItemListBox(VariablesManager.indexDest, itemTemp);
        }

        private void DeleteDestination_Click(object sender, RoutedEventArgs e)
        {
            if (-1 != VariablesManager.indexDest)
            {
                VariablesManager.actionManagers.listAction.RemoveAt(VariablesManager.indexDest);
                lbDest.ItemsSource = subFunctions.GetDestFromActionManager(VariablesManager.actionManagers);
                lbDest.SelectedIndex = lbDest.Items.Count - 1;
            }
        }

        #endregion

        #region Public functions

        public void LoadItemListBox(int index, delegateLoadItemListBox item)
        {
            if (null != item)
            {
                item(index);
                itemTemp = item;
            }
        }

        #endregion

        #region Members

        private delegateLoadItemListBox itemTemp;
        private SubFunctionsManager subFunctions = new SubFunctionsManager();

        #endregion
    }
}
