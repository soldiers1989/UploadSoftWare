﻿#pragma checksum "..\..\..\..\xaml\HeavyMetal\CurveWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "FC1AD1C599DBD05102058AD3B76A22C0"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1
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


namespace AIO.xaml.HeavyMetal {
    
    
    /// <summary>
    /// CurveWindow
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class CurveWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 7 "..\..\..\..\xaml\HeavyMetal\CurveWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.ChartPlotter plotter;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\xaml\HeavyMetal\CurveWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.Header dianwei;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\..\xaml\HeavyMetal\CurveWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal Microsoft.Research.DynamicDataDisplay.VerticalAxisTitle dianliu;
        
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
            System.Uri resourceLocater = new System.Uri("/DY-Detector;component/xaml/heavymetal/curvewindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\xaml\HeavyMetal\CurveWindow.xaml"
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
            
            #line 5 "..\..\..\..\xaml\HeavyMetal\CurveWindow.xaml"
            ((AIO.xaml.HeavyMetal.CurveWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.plotter = ((Microsoft.Research.DynamicDataDisplay.ChartPlotter)(target));
            return;
            case 3:
            this.dianwei = ((Microsoft.Research.DynamicDataDisplay.Header)(target));
            return;
            case 4:
            this.dianliu = ((Microsoft.Research.DynamicDataDisplay.VerticalAxisTitle)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

