﻿#pragma checksum "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "5BF8550C659B82156EF67B0E62AF5E2C32E0C5CC"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
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
    /// GszWindow
    /// </summary>
    public partial class GszWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtItems;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonAddItem;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonEditItem;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonDelItem;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonPrev;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonStartWork;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonRecord;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonBirefDescription;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPlayer;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnSaveProjects;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
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
            System.Uri resourceLocater = new System.Uri("/DY-Detector;component/xaml/ganshizhi/gszwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
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
            
            #line 10 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
            ((AIO.GszWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 17 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.SearchImage_MouseDown);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseLeave += new System.Windows.Input.MouseEventHandler(this.Image_MouseLeave);
            
            #line default
            #line hidden
            
            #line 17 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
            ((System.Windows.Controls.Image)(target)).MouseMove += new System.Windows.Input.MouseEventHandler(this.Image_MouseMove);
            
            #line default
            #line hidden
            return;
            case 3:
            this.txtItems = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.ButtonAddItem = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
            this.ButtonAddItem.Click += new System.Windows.RoutedEventHandler(this.ButtonAddItem_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButtonEditItem = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
            this.ButtonEditItem.Click += new System.Windows.RoutedEventHandler(this.ButtonEditItem_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ButtonDelItem = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
            this.ButtonDelItem.Click += new System.Windows.RoutedEventHandler(this.ButtonDelItem_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ButtonPrev = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
            this.ButtonPrev.Click += new System.Windows.RoutedEventHandler(this.ButtonPrev_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.ButtonStartWork = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
            this.ButtonStartWork.Click += new System.Windows.RoutedEventHandler(this.ButtonStartWork_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ButtonRecord = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
            this.ButtonRecord.Click += new System.Windows.RoutedEventHandler(this.ButtonRecord_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ButtonBirefDescription = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
            this.ButtonBirefDescription.Click += new System.Windows.RoutedEventHandler(this.ButtonBirefDescription_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnPlayer = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
            this.btnPlayer.Click += new System.Windows.RoutedEventHandler(this.btnPlayer_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.btnSaveProjects = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\xaml\Ganshizhi\GszWindow.xaml"
            this.btnSaveProjects.Click += new System.Windows.RoutedEventHandler(this.btnSaveProjects_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.WrapPanelItem = ((System.Windows.Controls.WrapPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

