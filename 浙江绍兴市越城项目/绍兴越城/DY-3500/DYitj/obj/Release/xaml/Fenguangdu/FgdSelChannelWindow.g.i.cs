﻿#pragma checksum "..\..\..\..\xaml\Fenguangdu\FgdSelChannelWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1795C6C7ACCC972034CDA1EE8FBE6DE9"
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
    /// FgdSelChannelWindow
    /// </summary>
    public partial class FgdSelChannelWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\..\..\xaml\Fenguangdu\FgdSelChannelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelTitle;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\xaml\Fenguangdu\FgdSelChannelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CheckBoxSelAll;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\xaml\Fenguangdu\FgdSelChannelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonPrev;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\xaml\Fenguangdu\FgdSelChannelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonNext;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\..\xaml\Fenguangdu\FgdSelChannelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox cb_SelAll;
        
        #line default
        #line hidden
        
        
        #line 58 "..\..\..\..\xaml\Fenguangdu\FgdSelChannelWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lb_selAll;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\xaml\Fenguangdu\FgdSelChannelWindow.xaml"
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
            System.Uri resourceLocater = new System.Uri("/DY-Detector;component/xaml/fenguangdu/fgdselchannelwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\xaml\Fenguangdu\FgdSelChannelWindow.xaml"
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
            
            #line 10 "..\..\..\..\xaml\Fenguangdu\FgdSelChannelWindow.xaml"
            ((AIO.FgdSelChannelWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.labelTitle = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.CheckBoxSelAll = ((System.Windows.Controls.CheckBox)(target));
            
            #line 19 "..\..\..\..\xaml\Fenguangdu\FgdSelChannelWindow.xaml"
            this.CheckBoxSelAll.Click += new System.Windows.RoutedEventHandler(this.CheckBoxSelAll_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ButtonPrev = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\xaml\Fenguangdu\FgdSelChannelWindow.xaml"
            this.ButtonPrev.Click += new System.Windows.RoutedEventHandler(this.ButtonPrev_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButtonNext = ((System.Windows.Controls.Button)(target));
            
            #line 21 "..\..\..\..\xaml\Fenguangdu\FgdSelChannelWindow.xaml"
            this.ButtonNext.Click += new System.Windows.RoutedEventHandler(this.ButtonNext_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.cb_SelAll = ((System.Windows.Controls.CheckBox)(target));
            
            #line 22 "..\..\..\..\xaml\Fenguangdu\FgdSelChannelWindow.xaml"
            this.cb_SelAll.Click += new System.Windows.RoutedEventHandler(this.cb_SelAll_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.lb_selAll = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.WrapPanelChannel = ((System.Windows.Controls.WrapPanel)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

