//! Copyright 2015 Virtium Technology, Inc.
//! All rights reserved
//!
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace PostBuildCopy.Classes
{
    public class VariablesManager
    {
        // ===================
        // static variables 
        // ===================
        private static string m_CurrentDirectory;
        public static string currentDirectory
        {
            set { m_CurrentDirectory = value; }
            get { return m_CurrentDirectory; }
        }


        private static int m_IndexListBox2 = new int();
        public static int indexDest
        {
            set { m_IndexListBox2 = value; }
            get { return m_IndexListBox2; }
        }


        private static List<string> m_Parameters = new List<string>();
        public static List<string> parameters
        {
            set { m_Parameters = value; }
            get { return m_Parameters; }
        }


        private static List<string> m_Filters = new List<string>();
        public static List<string> filters
        {
            set { m_Filters = value; }
            get { return m_Filters; }
        }


        private static List<StringPath> m_StrBranchsExplorer = new List<StringPath>();
        public static List<StringPath> strBranchsExplorer
        {
            set { m_StrBranchsExplorer = value; }
            get { return m_StrBranchsExplorer; }
        }


        private static List<StringPath> m_oldStringPaths = new List<StringPath>();
        public static List<StringPath> oldStringPaths
        {
            set { m_oldStringPaths = value; }
            get { return m_oldStringPaths; }
        }


        private static TreeViewItem m_TreeItemRoot = new TreeViewItem();
        public static TreeViewItem treeItemRoot
        {
            set { m_TreeItemRoot = value; }
            get { return m_TreeItemRoot; }
        }


        private static TreeViewItem m_DraggedItem = new TreeViewItem();
        public static TreeViewItem draggedItem
        {
            set { m_DraggedItem = value; }
            get { return m_DraggedItem; }
        }


        private static TreeViewItem m_Target = new TreeViewItem();
        public static TreeViewItem targetItem
        {
            set { m_Target = value; }
            get { return m_Target; }
        }


        private static ActionManager m_ActionManagers = new ActionManager();
        public static ActionManager actionManagers
        {
            set { m_ActionManagers = value; }
            get { return m_ActionManagers; }
        }
    }
}
