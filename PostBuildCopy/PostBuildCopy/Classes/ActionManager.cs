/*
<License>
Copyright 2015 Virtium Technology
Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
http ://www.apache.org/licenses/LICENSE-2.0
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
</License>
*/

using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace PostBuildCopy.Classes
{
    public class ActionManager
    {
        // Private member
        private static List<Action> s_Actions = new List<Action>();
        
        // Property 
        public static List<Action> Actions
        {
            get { return s_Actions; }
            set { s_Actions = value; }
        }

        // Actions variable hold all actions
        // This function used to get all destinations in the actions
        public static ObservableCollection<PathDataModel> GetListDestinationOfActionManager()
        {
            ObservableCollection<PathDataModel> destinationTemps = new ObservableCollection<PathDataModel>();
            foreach(CopySourcesToDestination copy in Actions)
                destinationTemps.Add(copy.Destination);
            return destinationTemps;
        }

        // Check each action have both sources in destination or no?
        // If not, output a message
        public static bool HasContentInActions(List<Action> iListAction)
        {
            foreach (object itemAction in iListAction)
            {
                if (0 == (itemAction as CopySourcesToDestination).Sources.Count)
                {
                    string destfree = (itemAction as CopySourcesToDestination).Destination.PathModel;
                    MessageBox.Show("\"" + destfree + "\"" + " have no sources", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                    return false;
                }
            }
            return true;
        }
    }
}
