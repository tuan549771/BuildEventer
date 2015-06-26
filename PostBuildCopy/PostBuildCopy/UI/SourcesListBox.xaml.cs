//! Copyright 2015 Virtium Technology, Inc.
//! All rights reserved
//!

using PostBuildCopy.Classes;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PostBuildCopy.UI
{
    public delegate void delegateLoadItemListBox(int indexItem);

    public partial class SourcesListBox : UserControl
    {
        #region Constructors

        public SourcesListBox()
        {
            InitializeComponent();
            VariablesManager.indexDest = -1;
        }

        #endregion

        #region Event handlers

        // This funtion used to add a item to ListBox and update actions
        private void DropSource(object sender, DragEventArgs e)
        {
            int indexDestination = VariablesManager.indexDest;
            if (-1 == indexDestination)
            {
                MessageBox.Show("First of all, You must choice a destination.","My App", MessageBoxButton.OK, MessageBoxImage.Information);
                return;
            }
            try
            {
                object path = e.Data.GetData(typeof(string));
                if (null != path)
                {
                    string pathSource = path.ToString();
                    pathSource = subFunctions.GetPathRoot(pathSource);
                    if (null == pathSource)
                        return;
                    ObservableCollection<Destination> destinationTemps = new ObservableCollection<Destination>();
                    ObservableCollection<Source> SourcesTemp = new ObservableCollection<Source>();
                    CopySourcesToDestination CopySourcesToDestTemp = new CopySourcesToDestination();
                    destinationTemps = subFunctions.GetDestFromActionManager(VariablesManager.actionManagers);
                    SourcesTemp = (VariablesManager.actionManagers.listAction[indexDestination] as CopySourcesToDestination).sources;
                    SourcesTemp.Add(new Source() { source = pathSource });
                    CopySourcesToDestTemp.destination = destinationTemps[indexDestination];
                    CopySourcesToDestTemp.sources = SourcesTemp;
                    VariablesManager.actionManagers.listAction[indexDestination] = (CopySourcesToDestTemp);
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
            if (0 != VariablesManager.actionManagers.listAction.Count)
                lbSources.ItemsSource = (VariablesManager.actionManagers.listAction[indexDestination] as CopySourcesToDestination).sources;
        }

        // This funtion used to delete a item ListBox and update actions
        private void DeleteSource_Click(object sender, RoutedEventArgs e)
        {
            int indexDest = VariablesManager.indexDest;
            int indexSources = lbSources.SelectedIndex;

            if ((-1 != indexDest) && (-1 != indexSources))
            {
                ObservableCollection<Destination> destinationTemps = subFunctions.GetDestFromActionManager(VariablesManager.actionManagers);
                ObservableCollection<Source> SourcesTemp = new ObservableCollection<Source>();
                CopySourcesToDestination CopySourcesToDestTemp = new CopySourcesToDestination();
                SourcesTemp = (VariablesManager.actionManagers.listAction[indexDest] as CopySourcesToDestination).sources;
                SourcesTemp.Remove(SourcesTemp[indexSources]);
                CopySourcesToDestTemp.destination = destinationTemps[indexDest];
                CopySourcesToDestTemp.sources = SourcesTemp;
                VariablesManager.actionManagers.listAction[indexDest] = (CopySourcesToDestTemp);
                lbSources.ItemsSource = (VariablesManager.actionManagers.listAction[indexDest] as CopySourcesToDestination).sources;
            }
        }

        #endregion

        #region Public functions

        public void LoadListBoxItem(int index, delegateLoadItemListBox item)
        {
            item(index);
        }

        public void LoadListBoxItemSources1(int index)
        {
            if (-1 != index)
                lbSources.ItemsSource = (VariablesManager.actionManagers.listAction[index] as CopySourcesToDestination).sources;
            else
                lbSources.ItemsSource = null;
        }

        #endregion

        #region Members

        SubFunctionsManager subFunctions = new SubFunctionsManager();

        #endregion
    }
}
