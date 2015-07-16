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

namespace PostBuildCopy.Classes
{
    public class CouplePath
    {
        // Private member
        // A CouplePath include both sub path and parent path
        private string m_ParentPath;
        private string m_SubPath;

        // Property
        public string ParentPath { get { return m_ParentPath; } set { m_ParentPath = value; } }
        public string SubPath { get { return m_SubPath; } set { m_SubPath = value; } }

        // Constructor
        public CouplePath(string iParentPath, string iSubPath)
        {
            m_ParentPath = iParentPath;
            m_SubPath = iSubPath;
        }

        // Constructor
        public CouplePath(){
        }
    }
}
