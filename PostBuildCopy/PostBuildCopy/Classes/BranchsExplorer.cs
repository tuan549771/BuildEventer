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
        private static List<CouplePath> branchsExplorers = new List<CouplePath>();

        // Get all branches in branchsExplorers
        public static List<CouplePath> GetBranchsExplorers()
        {
            return branchsExplorers;
        }

        // Set a branch of explorer into branchsExplorers variable
        public static void SetOneBranchsExplorer(CouplePath iCouplePath)
        {
            foreach (CouplePath couplePath in branchsExplorers)
            {
                if ((couplePath.ParentPath == iCouplePath.ParentPath) && (couplePath.SubPath == iCouplePath.SubPath))
                    return;
            }
            branchsExplorers.Add(iCouplePath);
        }

        // Set all branches of branchsExplorers variable
        public static void SetBranchsExplorers(List<CouplePath> iCouplePaths)
        {
            branchsExplorers = iCouplePaths;
        }

        // Get and return all sub paths of a parent path
        public static List<string> GetSubPathExplorer(string iParentPath)
        {
            List<string> subPaths = new List<string>();
            foreach (CouplePath couplePath in branchsExplorers)
            {
                if (iParentPath == couplePath.ParentPath)
                    subPaths.Add(couplePath.SubPath);
            }
            return subPaths;
        }
    }

}
