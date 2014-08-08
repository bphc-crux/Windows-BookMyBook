using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.UI.ApplicationSettings;
using Windows.ApplicationModel.Resources.Core;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BookMyBook
{
    public sealed partial class fly : UserControl
    {
        public fly()
        {
            this.InitializeComponent();
            ChangeLanguage();
        }
        public void ChangeLanguage()
        {
            String t1 = "",t2="",t3="",t4="",t5="",t6="";
            ResourceMap resourceMap;
            if (MainPage.currentLang == 1) resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Hindi");
            else if (MainPage.currentLang == 2) resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("French");
            else if (MainPage.currentLang == 3) resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Spanish");
            else resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("English");
            t1 = resourceMap.GetValue("Privacy1").ValueAsString;
            t2 = resourceMap.GetValue("Privacy2").ValueAsString;
            t3 = resourceMap.GetValue("Privacy3").ValueAsString;
            t4 = resourceMap.GetValue("Privacy4").ValueAsString;
            t5 = resourceMap.GetValue("Privacy5").ValueAsString;
            t6 = resourceMap.GetValue("Privacy6").ValueAsString;
            privacyStatementText.Text =( t1 + "\n" + t2 + "\n*" + t3 + "\n*" + t4 + "\n*" + t5 + "\n*" + t6).Replace("\n",Environment.NewLine).ToString(); 
        }
        
        private void PrivacyBackClicked(object sender, RoutedEventArgs e)
        {
            if (this.Parent.GetType() == typeof(Popup))
            {
                ((Popup)this.Parent).IsOpen = false;
            }
            SettingsPane.Show();
        }


    }
}
