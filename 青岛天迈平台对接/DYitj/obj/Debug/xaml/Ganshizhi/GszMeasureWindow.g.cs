﻿#pragma checksum "..\..\..\..\xaml\Ganshizhi\GszMeasureWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4AFEDF525A640EB8F00F0B6A7D2E9C045FDA6B7A"
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
    /// GszMeasureWindow
    /// </summary>
    public partial class GszMeasureWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 26 "..\..\..\..\xaml\Ganshizhi\GszMeasureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonPrev;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\xaml\Ganshizhi\GszMeasureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonSampleTest;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\xaml\Ganshizhi\GszMeasureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPlayer;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\xaml\Ganshizhi\GszMeasureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonBirefDescription;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\xaml\Ganshizhi\GszMeasureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel WrapPanelChannel;
        
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
            System.Uri resourceLocater = new System.Uri("/DY-Detector;component/xaml/ganshizhi/gszmeasurewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\xaml\Ganshizhi\GszMeasureWindow.xaml"
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
            
            #line 10 "..\..\..\..\xaml\Ganshizhi\GszMeasureWindow.xaml"
            ((AIO.GszMeasureWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\..\xaml\Ganshizhi\GszMeasureWindow.xaml"
            ((AIO.GszMeasureWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.ButtonPrev = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\..\xaml\Ganshizhi\GszMeasureWindow.xaml"
            this.ButtonPrev.Click += new System.Windows.RoutedEventHandler(this.ButtonPrev_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ButtonSampleTest = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\..\xaml\Ganshizhi\GszMeasureWindow.xaml"
            this.ButtonSampleTest.Click += new System.Windows.RoutedEventHandler(this.ButtonSampleTest_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnPlayer = ((System.Windows.Controls.Button)(target));
            
            #line 28 "..\..\..\..\xaml\Ganshizhi\GszMeasureWindow.xaml"
            this.btnPlayer.Click += new System.Windows.RoutedEventHandler(this.btnPlayer_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButtonBirefDescription = ((System.Windows.Controls.Button)(target));
            
            #line 29 "..\..\..\..\xaml\Ganshizhi\GszMeasureWindow.xaml"
            this.ButtonBirefDescription.Click += new System.Windows.RoutedEventHandler(this.ButtonBirefDescription_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.WrapPanelChannel = ((System.Windows.Controls.WrapPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

