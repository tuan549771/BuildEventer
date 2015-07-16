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

namespace PostBuildCopy.Classes
{
    class BranchsExplorer
    {
        // Private member
        // BranchsExplorers hold branches of explorer that
        // We dragged and dropped into explorer
        // Include a couple path
        // Parent path and sub path
        private static List<CouplePath> s_m_BranchsExplorers = new List<CouplePath>();

        // Get all branches in branchsExplorers
        public static List<CouplePath> GetBranchsExplorers()
        {
            return s_m_BranchsExplorers;
        }

        // Set a branch of explorer into branchsExplorers variable
        public static void SetOneBranchsExplorer(CouplePath iCouplePath)
        {
            foreach (CouplePath couplePath in s_m_BranchsExplorers)
            {
                if ((couplePath.ParentPath == iCouplePath.ParentPath) && (couplePath.SubPath == iCouplePath.SubPath))
                    return;
            }
            s_m_BranchsExplorers.Add(iCouplePath);
        }

        // Set all branches of branchsExplorers variable
        public static void SetBranchsExplorers(List<CouplePath> iCouplePaths)
        {
            s_m_BranchsExplorers = iCouplePaths;
        }

        // Get and return all sub paths of a parent path
        public static List<string> GetSubPathExplorer(string iParentPath)
        {
            List<string> subPaths = new List<string>();
            foreach (CouplePath couplePath in s_m_BranchsExplorers)
            {
                if (iParentPath == couplePath.ParentPath)
                    subPaths.Add(couplePath.SubPath);
            }
            return subPaths;
        }
    }

}
