﻿#pragma checksum "..\..\..\..\xaml\Main\LoginWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "9C1657FE044E802CC9D8B32455E45CC5B2FD20A9"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using AIO.Properties;
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
    /// LoginWindow
    /// </summary>
    public partial class LoginWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 25 "..\..\..\..\xaml\Main\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label labelName;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\..\..\xaml\Main\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TextBoxUserName;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\xaml\Main\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.PasswordBox TextBoxUserPassword;
        
        #line default
        #line hidden
        
        
        #line 44 "..\..\..\..\xaml\Main\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonLogin;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\..\xaml\Main\LoginWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ButtonExit;
        
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
            System.Uri resourceLocater = new System.Uri("/DY-Detector;component/xaml/main/loginwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\xaml\Main\LoginWindow.xaml"
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
            
            #line 11 "..\..\..\..\xaml\Main\LoginWindow.xaml"
            ((AIO.LoginWindow)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.labelName = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.TextBoxUserName = ((System.Windows.Controls.TextBox)(target));
            
            #line 37 "..\..\..\..\xaml\Main\LoginWindow.xaml"
            this.TextBoxUserName.KeyDown += new System.Windows.Input.KeyEventHandler(this.TextBoxUserName_KeyDown);
            
            #line default
            #line hidden
            
            #line 37 "..\..\..\..\xaml\Main\LoginWindow.xaml"
            this.TextBoxUserName.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBoxUserName_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TextBoxUserPassword = ((System.Windows.Controls.PasswordBox)(target));
            
            #line 41 "..\..\..\..\xaml\Main\LoginWindow.xaml"
            this.TextBoxUserPassword.KeyDown += new System.Windows.Input.KeyEventHandler(this.TextBoxUserPassword_KeyDown);
            
            #line default
            #line hidden
            
            #line 41 "..\..\..\..\xaml\Main\LoginWindow.xaml"
            this.TextBoxUserPassword.PasswordChanged += new System.Windows.RoutedEventHandler(this.TextBoxUserPassword_PasswordChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.ButtonLogin = ((System.Windows.Controls.Button)(target));
            
            #line 44 "..\..\..\..\xaml\Main\LoginWindow.xaml"
            this.ButtonLogin.Click += new System.Windows.RoutedEventHandler(this.ButtonLogin_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.ButtonExit = ((System.Windows.Controls.Button)(target));
            
            #line 45 "..\..\..\..\xaml\Main\LoginWindow.xaml"
            this.ButtonExit.Click += new System.Windows.RoutedEventHandler(this.ButtonExit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

