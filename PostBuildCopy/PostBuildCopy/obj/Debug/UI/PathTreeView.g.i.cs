﻿#pragma checksum "..\..\..\UI\PathTreeView.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "627C2417AD4A070127352B19C514FDCB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18408
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using PostBuildCopy.Classes;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace PostBuildCopy.UI {
    
    
    /// <summary>
    /// PathTreeView
    /// </summary>
    public partial class PathTreeView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector, System.Windows.Markup.IStyleConnector {
        
        
        #line 8 "..\..\..\UI\PathTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal PostBuildCopy.UI.PathTreeView UCPathTreeView;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\UI\PathTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TreeView treeView;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\UI\PathTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem newItem;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\..\UI\PathTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem deleteItem;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\UI\PathTreeView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem refreshItem;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/PostBuildCopy;component/ui/pathtreeview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UI\PathTreeView.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.UCPathTreeView = ((PostBuildCopy.UI.PathTreeView)(target));
            return;
            case 2:
            this.treeView = ((System.Windows.Controls.TreeView)(target));
            
            #line 12 "..\..\..\UI\PathTreeView.xaml"
            this.treeView.AddHandler(System.Windows.Controls.TreeViewItem.ExpandedEvent, new System.Windows.RoutedEventHandler(this.TreeExpanded));
            
            #line default
            #line hidden
            
            #line 13 "..\..\..\UI\PathTreeView.xaml"
            this.treeView.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TreeMouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.newItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 36 "..\..\..\UI\PathTreeView.xaml"
            this.newItem.Click += new System.Windows.RoutedEventHandler(this.TreeNewItem);
            
            #line default
            #line hidden
            return;
            case 5:
            this.deleteItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 40 "..\..\..\UI\PathTreeView.xaml"
            this.deleteItem.Click += new System.Windows.RoutedEventHandler(this.TreeDeleteItem);
            
            #line default
            #line hidden
            return;
            case 6:
            this.refreshItem = ((System.Windows.Controls.MenuItem)(target));
            
            #line 44 "..\..\..\UI\PathTreeView.xaml"
            this.refreshItem.Click += new System.Windows.RoutedEventHandler(this.TreeRefreshItem);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        void System.Windows.Markup.IStyleConnector.Connect(int connectionId, object target) {
            System.Windows.EventSetter eventSetter;
            switch (connectionId)
            {
            case 3:
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.DropEvent;
            
            #line 24 "..\..\..\UI\PathTreeView.xaml"
            eventSetter.Handler = new System.Windows.DragEventHandler(this.TreeDrop);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.MouseMoveEvent;
            
            #line 25 "..\..\..\UI\PathTreeView.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseEventHandler(this.TreeMouseMove);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            eventSetter = new System.Windows.EventSetter();
            eventSetter.Event = System.Windows.UIElement.MouseRightButtonDownEvent;
            
            #line 26 "..\..\..\UI\PathTreeView.xaml"
            eventSetter.Handler = new System.Windows.Input.MouseButtonEventHandler(this.TreeMouseRight);
            
            #line default
            #line hidden
            ((System.Windows.Style)(target)).Setters.Add(eventSetter);
            break;
            }
        }
    }
}

