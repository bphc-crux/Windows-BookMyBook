﻿

#pragma checksum "C:\Users\Srujan Jha\Documents\Visual Studio 2013\Projects\Windows8\BookMyBook\BookMyBook.WindowsPhone\MainPage.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "30C9A842F3A05E45672BECBC714D7B52"
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
                #line 12 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.Primitives.ButtonBase)(target)).Click += this.Search_Click;
                 #line default
                 #line hidden
                break;
            case 2:
                #line 17 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target)).TextChanged += this.AutoSuggestBox_TextChanged;
                 #line default
                 #line hidden
                #line 17 "..\..\MainPage.xaml"
                ((global::Windows.UI.Xaml.Controls.AutoSuggestBox)(target)).SuggestionChosen += this.AutoSuggestBox_SuggestionChosen;
                 #line default
                 #line hidden
                break;
            }
            this._contentLoaded = true;
        }
    }
}


