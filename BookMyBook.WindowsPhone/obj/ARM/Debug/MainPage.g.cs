﻿

#pragma checksum "C:\Users\Srujan Jha\Documents\Visual Studio 2013\Projects\Windows8\BookMyBook\BookMyBook.WindowsPhone\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "7A0FCC0D214A5B4DB71718FB128FCDFB"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace BookMyBook
{
    partial class MainPage : global::Windows.UI.Xaml.Controls.Page, global::Windows.UI.Xaml.Markup.IComponentConnector
    {
        [global::System.CodeDom.Compiler.GeneratedCodeAttribute("Microsoft.Windows.UI.Xaml.Build.Tasks"," 4.0.0.0")]
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
 
        public void Connect(int connectionId, object target)
        {
            switch(connectionId)
            {
            case 1:
                #line 73 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.Selector)(target)).SelectionChanged += this.Tutorial_SelectionChanged;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 23 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.UIElement)(target)).Tapped += this.Search_Tapped;
                 #line default
                 #line hidden
                break;
            case 3:
                #line 90 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Search_Click;
                 #line default
                 #line hidden
                break;
            case 4:
                #line 93 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Tutorial_event;
                 #line default
                 #line hidden
                break;
            case 5:
                #line 94 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.about_event;
                 #line default
                 #line hidden
                break;
            case 6:
                #line 95 "..\..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Feedback_event;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}

