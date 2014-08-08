using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.ApplicationModel.Resources.Core;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace BookMyBook
{
    public sealed partial class AppbarContent : UserControl
    {
        Frame Frame = Window.Current.Content as Frame;
        public AppbarContent()
        {
            this.InitializeComponent();
                
        }
        
        private async void Flipkart()
        {
            Uri uri = new Uri("http://www.flipkart.com");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        private async void Infibeam()
        {
            Uri uri = new Uri("http://www.infibeam.com");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        private async void Landmark()
        {
            Uri uri = new Uri("http://www.landmark.com");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        private async void Amazon()
        {
            Uri uri = new Uri("http://www.amazon.com");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        private async void Uread()
        {
            Uri uri = new Uri("http://www.uread.com");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        private async void Crossword()
        {
            Uri uri = new Uri("http://www.crossword.com");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        private async void Homeshop18()
        {
            Uri uri = new Uri("http://www.homeshop18.com");
            await Windows.System.Launcher.LaunchUriAsync(uri);
        }
        
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }
        private void Button_Click_2(object sender, RoutedEventArgs e) { Flipkart();}
        private void Button_Click_3(object sender, RoutedEventArgs e) { Infibeam(); }
        private void Button_Click_4(object sender, RoutedEventArgs e) { Amazon(); }
        private void Button_Click_5(object sender, RoutedEventArgs e) { Homeshop18(); }
        private void Button_Click_6(object sender, RoutedEventArgs e) { Uread(); }
        private void Button_Click_7(object sender, RoutedEventArgs e) { Crossword(); }
        private void Button_Click_8(object sender, RoutedEventArgs e) { Landmark(); }
        
       
    }
}
