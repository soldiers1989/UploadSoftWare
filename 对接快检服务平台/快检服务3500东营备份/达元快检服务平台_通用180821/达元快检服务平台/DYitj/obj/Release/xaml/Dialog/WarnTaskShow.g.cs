﻿#pragma checksum "..\..\..\..\xaml\Dialog\WarnTaskShow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "77A46EDCB4D72E27A0BF1688122A349A"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.34209
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
    /// WarnTaskShow
    /// </summary>
    public partial class WarnTaskShow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 20 "..\..\..\..\xaml\Dialog\WarnTaskShow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonAddItem;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\xaml\Dialog\WarnTaskShow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonEditItem;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\xaml\Dialog\WarnTaskShow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonDelItem;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\xaml\Dialog\WarnTaskShow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonPrev;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\xaml\Dialog\WarnTaskShow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel WrapPanelItem;
        
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
            System.Uri resourceLocater = new System.Uri("/DY-Detector;component/xaml/dialog/warntaskshow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\xaml\Dialog\WarnTaskShow.xaml"
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
            
            #line 10 "..\..\..\..\xaml\Dialog\WarnTaskShow.xaml"
            ((AIO.WarnTaskShow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ButtonAddItem = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\xaml\Dialog\WarnTaskShow.xaml"
            this.ButtonAddItem.Click += new System.Windows.RoutedEventHandler(this.ButtonAddItem_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonEditItem = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\..\xaml\Dialog\WarnTaskShow.xaml"
            this.ButtonEditItem.Click += new System.Windows.RoutedEventHandler(this.ButtonEditItem_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ButtonDelItem = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\xaml\Dialog\WarnTaskShow.xaml"
            this.ButtonDelItem.Click += new System.Windows.RoutedEventHandler(this.ButtonDelItem_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButtonPrev = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\xaml\Dialog\WarnTaskShow.xaml"
            this.ButtonPrev.Click += new System.Windows.RoutedEventHandler(this.ButtonPrev_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.WrapPanelItem = ((System.Windows.Controls.WrapPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

