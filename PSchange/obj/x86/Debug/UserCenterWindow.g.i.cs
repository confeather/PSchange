﻿#pragma checksum "..\..\..\UserCenterWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "E756263E66FF366DAA017F46CC344B3FDFC5698C"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.42000
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using PSchange;
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


namespace PSchange {
    
    
    /// <summary>
    /// UserCenterWindow
    /// </summary>
    public partial class UserCenterWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 11 "..\..\..\UserCenterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button confirmBtn;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\UserCenterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userNickName;
        
        #line default
        #line hidden
        
        
        #line 14 "..\..\..\UserCenterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userPhone;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\UserCenterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userQQ;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\UserCenterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userEmail;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\UserCenterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userAddress;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\UserCenterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button updateBtn;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\UserCenterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button cancelBtn;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\..\UserCenterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button logoutBtn;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\UserCenterWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox userPasswd;
        
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
            System.Uri resourceLocater = new System.Uri("/PSchange;component/usercenterwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\UserCenterWindow.xaml"
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
            this.confirmBtn = ((System.Windows.Controls.Button)(target));
            
            #line 11 "..\..\..\UserCenterWindow.xaml"
            this.confirmBtn.Click += new System.Windows.RoutedEventHandler(this.confirmBtn_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.userNickName = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.userPhone = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.userQQ = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.userEmail = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.userAddress = ((System.Windows.Controls.TextBox)(target));
            return;
            case 7:
            this.updateBtn = ((System.Windows.Controls.Button)(target));
            
            #line 22 "..\..\..\UserCenterWindow.xaml"
            this.updateBtn.Click += new System.Windows.RoutedEventHandler(this.updateBtn_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.cancelBtn = ((System.Windows.Controls.Button)(target));
            
            #line 23 "..\..\..\UserCenterWindow.xaml"
            this.cancelBtn.Click += new System.Windows.RoutedEventHandler(this.cancelBtn_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.logoutBtn = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\UserCenterWindow.xaml"
            this.logoutBtn.Click += new System.Windows.RoutedEventHandler(this.logoutBtn_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.userPasswd = ((System.Windows.Controls.TextBox)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

