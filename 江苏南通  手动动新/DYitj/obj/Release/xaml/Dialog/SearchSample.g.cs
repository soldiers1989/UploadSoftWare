﻿#pragma checksum "..\..\..\..\xaml\Dialog\SearchSample.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "D76BA4800E9E4A13622992A3DC253238"
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


namespace AIO.xaml.Dialog {
    
    
    /// <summary>
    /// SearchSample
    /// </summary>
    public partial class SearchSample : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 6 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label label1;
        
        #line default
        #line hidden
        
        
        #line 7 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox textBoxName;
        
        #line default
        #line hidden
        
        
        #line 8 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Search;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnChoose;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAdd;
        
        #line default
        #line hidden
        
        
        #line 11 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid DataGridRecord;
        
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
            System.Uri resourceLocater = new System.Uri("/DY-Detector;component/xaml/dialog/searchsample.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
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
            
            #line 4 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
            ((AIO.xaml.Dialog.SearchSample)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.label1 = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.textBoxName = ((System.Windows.Controls.TextBox)(target));
            
            #line 7 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
            this.textBoxName.KeyDown += new System.Windows.Input.KeyEventHandler(this.textBoxName_KeyDown);
            
            #line default
            #line hidden
            
            #line 7 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
            this.textBoxName.SelectionChanged += new System.Windows.RoutedEventHandler(this.textBoxName_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.Search = ((System.Windows.Controls.Button)(target));
            
            #line 8 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
            this.Search.Click += new System.Windows.RoutedEventHandler(this.Search_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnChoose = ((System.Windows.Controls.Button)(target));
            
            #line 9 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
            this.btnChoose.Click += new System.Windows.RoutedEventHandler(this.btnChoose_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnAdd = ((System.Windows.Controls.Button)(target));
            
            #line 10 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
            this.btnAdd.Click += new System.Windows.RoutedEventHandler(this.btnAdd_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.DataGridRecord = ((System.Windows.Controls.DataGrid)(target));
            
            #line 11 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
            this.DataGridRecord.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.DataGridRecord_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 11 "..\..\..\..\xaml\Dialog\SearchSample.xaml"
            this.DataGridRecord.LoadingRow += new System.EventHandler<System.Windows.Controls.DataGridRowEventArgs>(this.DataGridRecord_LoadingRow);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

