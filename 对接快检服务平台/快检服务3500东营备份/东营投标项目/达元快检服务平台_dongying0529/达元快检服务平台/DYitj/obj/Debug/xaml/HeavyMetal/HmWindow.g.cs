﻿#pragma checksum "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "4CCD709565D369E6131770CBCDD8910C"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

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


namespace AIO {
    
    
    /// <summary>
    /// HmWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class HmWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lbTitle;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonAddItem;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonEditItem;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonDelItem;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonPrev;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonStartWork;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonRecord;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveProjects;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel WrapPanelItem;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DY-Detector;component/xaml/heavymetal/hmwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 10 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
            ((AIO.HmWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.lbTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.Image = ((System.Windows.Controls.Image)(target));
            
            #line 17 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
            this.Image.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Image_MouseDown);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ButtonAddItem = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
            this.ButtonAddItem.Click += new System.Windows.RoutedEventHandler(this.ButtonAddItem_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButtonEditItem = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
            this.ButtonEditItem.Click += new System.Windows.RoutedEventHandler(this.ButtonEditItem_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ButtonDelItem = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
            this.ButtonDelItem.Click += new System.Windows.RoutedEventHandler(this.ButtonDelItem_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ButtonPrev = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
            this.ButtonPrev.Click += new System.Windows.RoutedEventHandler(this.ButtonPrev_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ButtonStartWork = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
            this.ButtonStartWork.Click += new System.Windows.RoutedEventHandler(this.ButtonStartWork_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ButtonRecord = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
            this.ButtonRecord.Click += new System.Windows.RoutedEventHandler(this.ButtonRecord_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnSaveProjects = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\..\xaml\HeavyMetal\HmWindow.xaml"
            this.btnSaveProjects.Click += new System.Windows.RoutedEventHandler(this.btnSaveProjects_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.WrapPanelItem = ((System.Windows.Controls.WrapPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

