﻿#pragma checksum "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "384B184BC5524E308F0DDFF93EC5E88E2FD52894"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Research.DynamicDataDisplay;
using Microsoft.Research.DynamicDataDisplay.Charts;
using Microsoft.Research.DynamicDataDisplay.Charts.Axes;
using Microsoft.Research.DynamicDataDisplay.Charts.Navigation;
using Microsoft.Research.DynamicDataDisplay.Charts.Shapes;
using Microsoft.Research.DynamicDataDisplay.Common.Palettes;
using Microsoft.Research.DynamicDataDisplay.DataSources;
using Microsoft.Research.DynamicDataDisplay.Navigation;
using Microsoft.Research.DynamicDataDisplay.PointMarkers;
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
    /// GszReportWindow
    /// </summary>
    public partial class GszReportWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label LabelInfo;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonPrint;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonPrev;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button button1;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btn_upload;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Btn_ShowDatas;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
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
            System.Uri resourceLocater = new System.Uri("/DY-Detector;component/xaml/ganshizhi/gszreportwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
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
            
            #line 12 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
            ((AIO.GszReportWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            
            #line 12 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
            ((AIO.GszReportWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.LabelInfo = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.ButtonPrint = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
            this.ButtonPrint.Click += new System.Windows.RoutedEventHandler(this.ButtonPrint_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ButtonPrev = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
            this.ButtonPrev.Click += new System.Windows.RoutedEventHandler(this.ButtonPrev_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.button1 = ((System.Windows.Controls.Button)(target));
            
            #line 25 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
            this.button1.Click += new System.Windows.RoutedEventHandler(this.ButtonSave_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btn_upload = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
            this.btn_upload.Click += new System.Windows.RoutedEventHandler(this.btn_upload_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Btn_ShowDatas = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\..\..\xaml\Ganshizhi\GszReportWindow.xaml"
            this.Btn_ShowDatas.Click += new System.Windows.RoutedEventHandler(this.Btn_ShowDatas_Click);
            
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

