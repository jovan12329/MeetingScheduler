﻿#pragma checksum "..\..\..\Views\AppointmentView.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "FFFF0F839B1121510A6FADF5D8E173EEBF5FD27215581C263A578B2C6BE9EDB0"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Meeting_Scheduler.Views;
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


namespace Meeting_Scheduler.Views {
    
    
    /// <summary>
    /// AppointmentView
    /// </summary>
    public partial class AppointmentView : System.Windows.Controls.UserControl, System.Windows.Markup.IComponentConnector {
        
        
        #line 44 "..\..\..\Views\AppointmentView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox Type;
        
        #line default
        #line hidden
        
        
        #line 66 "..\..\..\Views\AppointmentView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox start;
        
        #line default
        #line hidden
        
        
        #line 76 "..\..\..\Views\AppointmentView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox end;
        
        #line default
        #line hidden
        
        
        #line 87 "..\..\..\Views\AppointmentView.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox employees;
        
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
            System.Uri resourceLocater = new System.Uri("/Meeting_Scheduler;component/views/appointmentview.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\Views\AppointmentView.xaml"
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
            this.Type = ((System.Windows.Controls.ComboBox)(target));
            
            #line 46 "..\..\..\Views\AppointmentView.xaml"
            this.Type.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Type_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.start = ((System.Windows.Controls.ComboBox)(target));
            
            #line 68 "..\..\..\Views\AppointmentView.xaml"
            this.start.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.start_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.end = ((System.Windows.Controls.ComboBox)(target));
            
            #line 78 "..\..\..\Views\AppointmentView.xaml"
            this.end.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.end_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.employees = ((System.Windows.Controls.ListBox)(target));
            
            #line 90 "..\..\..\Views\AppointmentView.xaml"
            this.employees.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.employees_SelectionChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

