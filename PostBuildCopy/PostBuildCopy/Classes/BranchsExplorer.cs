using System.Collections.Generic;

namespace PostBuildCopy.Classes
{
    class BranchsExplorer
    {
        private static List<CouplePath> branchsExplorers = new List<CouplePath>();

        public static List<CouplePath> GetBranchsExplorers()
        {
            return branchsExplorers;
        }

        public static void SetOneBranchsExplorer(CouplePath iCouplePath)
        {
            foreach (CouplePath couplePath in branchsExplorers)
            {
                if ((couplePath.ParentPath == iCouplePath.ParentPath) && (couplePath.SubPath == iCouplePath.SubPath))
                    return;
            }
            branchsExplorers.Add(iCouplePath);
        }

        public static void SetBranchsExplorers(List<CouplePath> iCouplePaths)
        {
            branchsExplorers = iCouplePaths;
        }

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
