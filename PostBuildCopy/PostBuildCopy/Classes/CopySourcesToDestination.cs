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

using System.Collections.ObjectModel;

namespace PostBuildCopy.Classes
{
    /// <summary>
    /// 
    /// A CopySourcesToDestination hold
    /// A destination and many sources 
    /// 
    /// </summary>
    public class CopySourcesToDestination : Action
    {
        // private members
        private PathDataModel m_Destination;
        private ObservableCollection<PathDataModel> m_Sources = new ObservableCollection<PathDataModel>();

        // Property 
        public PathDataModel Destination
        {
            get { return m_Destination; }
            set { m_Destination = value; }
        }

        // Property 
        public ObservableCollection<PathDataModel> Sources
        {
            get { return m_Sources; }
            set { m_Sources = value; }
        }

        // Constructor
        public CopySourcesToDestination(PathDataModel iDestination)
        {
            Destination = iDestination;
            Sources = new ObservableCollection<PathDataModel>();
        }
    }
}
