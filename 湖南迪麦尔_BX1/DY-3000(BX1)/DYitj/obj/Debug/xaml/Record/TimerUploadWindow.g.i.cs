﻿#pragma checksum "..\..\..\..\xaml\Record\TimerUploadWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7CD0FBE17D27DB0D2F775BCFA1B927C2F33BEB9E"
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


namespace AIO.xaml.Record {
    
    
    /// <summary>
    /// TimerUploadWindow
    /// </summary>
    public partial class TimerUploadWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\..\xaml\Record\TimerUploadWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LbUploadInfo;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\..\xaml\Record\TimerUploadWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_Suspended;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\..\xaml\Record\TimerUploadWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Bnt_Stop;
        
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
            System.Uri resourceLocater = new System.Uri("/DY-Detector;component/xaml/record/timeruploadwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\xaml\Record\TimerUploadWindow.xaml"
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
            
            #line 5 "..\..\..\..\xaml\Record\TimerUploadWindow.xaml"
            ((AIO.xaml.Record.TimerUploadWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LbUploadInfo = ((System.Windows.Controls.Label)(target));
            
            #line 8 "..\..\..\..\xaml\Record\TimerUploadWindow.xaml"
            this.LbUploadInfo.MouseMove += new System.Windows.Input.MouseEventHandler(this.LbUploadInfo_MouseMove);
            
            #line default
            #line hidden
            
            #line 8 "..\..\..\..\xaml\Record\TimerUploadWindow.xaml"
            this.LbUploadInfo.MouseLeave += new System.Windows.Input.MouseEventHandler(this.LbUploadInfo_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Btn_Suspended = ((System.Windows.Controls.Button)(target));
            return;
            case 4:
            this.Bnt_Stop = ((System.Windows.Controls.Button)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

