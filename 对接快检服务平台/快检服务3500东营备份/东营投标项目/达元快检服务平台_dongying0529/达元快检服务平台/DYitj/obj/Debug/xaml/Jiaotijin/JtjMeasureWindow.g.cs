﻿#pragma checksum "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7BB08A20FB40C9A6F7340557517D3C51"
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
    /// JtjMeasureWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class JtjMeasureWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labTitle;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelInfo;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonPrev;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonSampleTest;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnPlayer;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonBirefDescription;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.WrapPanel WrapPanelChannel;
        
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
            System.Uri resourceLocater = new System.Uri("/DY-Detector;component/xaml/jiaotijin/jtjmeasurewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml"
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
            
            #line 10 "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml"
            ((AIO.JtjMeasureWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 10 "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml"
            ((AIO.JtjMeasureWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.labTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.LabelInfo = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.ButtonPrev = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml"
            this.ButtonPrev.Click += new System.Windows.RoutedEventHandler(this.ButtonPrev_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButtonSampleTest = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml"
            this.ButtonSampleTest.Click += new System.Windows.RoutedEventHandler(this.ButtonSampleTest_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnPlayer = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml"
            this.btnPlayer.Click += new System.Windows.RoutedEventHandler(this.btnPlayer_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.ButtonBirefDescription = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\xaml\Jiaotijin\JtjMeasureWindow.xaml"
            this.ButtonBirefDescription.Click += new System.Windows.RoutedEventHandler(this.ButtonBirefDescription_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.WrapPanelChannel = ((System.Windows.Controls.WrapPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

