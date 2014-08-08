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
using Windows.UI.Popups;
using Windows.System;
using AdDuplex;
using Windows.ApplicationModel.Resources.Core;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace BookMyBook
{
    public sealed partial class MainPage : Page
    {
        public static String s = "";
        public MainPage()
        {
            this.InitializeComponent();
            Enter.QueryText = "";
            Window.Current.SizeChanged += Window_SizeChanged;
        }
        private void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
            var rect = Window.Current.Bounds;
            PageTitle.Width = rect.Width - 340.0;
        }
        private Boolean checkisbn(long n,int l)
        {
            long m = n;
            m = m / 10;
            int sum = 0;
            if (l == 10)
            {
                for (int i = 1; i < 10; i++)
                {
                    sum = sum + (int)((10-i) * (m % 10));
                    m /= 10;
                }
                sum = sum % 11;
            }
            else if (l == 13)
            {
                for (int i = 1; i < 13; i++)
                {
                    if(i%2==0)
                    sum = sum + (int)(1 * (m % 10));
                    else sum = sum + (int)(3 * (m % 10));
                    m /= 10;
                }
                sum =10- sum % 10;
            }
            if (sum == (n % 10))
                return true;
            else return false;
        }
        private Boolean check()
        {
            s = Enter.QueryText;
            if (!(s.Length == 10 || s.Length == 13)) {  return false; }
            try
            {
                long d = Convert.ToInt64(s);
                if (!checkisbn(d, s.Length))
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
        private void ShowPopupAnimationClicked(String s)
        {
            if (!LightDismissAnimatedPopup.IsOpen)
            {
                errortext.Text = s;
                LightDismissAnimatedPopup.IsOpen = true;
            }
        }
        private void CloseAnimatedPopupClicked(object sender, RoutedEventArgs e)
        {
            if (LightDismissAnimatedPopup.IsOpen) { LightDismissAnimatedPopup.IsOpen = false; }
        }
        private void pop(String s)
        {
            ShowPopupAnimationClicked("OOPS :( :( :(\n" + s);
        }
        private void Grid_KeyUp(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Enter) {  String text = Enter.QueryText;
            text = text.Replace("-", "");
            Enter.QueryText = text;
            if (text.Equals("")) { ShowPopupAnimationClicked("OOPS :( :( :(\nSearch by title, author, publisher or ISBN of the book."); return; }
            if (check())
            {
                if (App.IsInternetAvailable)
                {
                    if (this.Frame != null)
                    {
                        s = text;
                        this.Frame.Navigate(typeof(SplitPage1), s);
                    }
                }
                else { pop("No internet connection found."); }
            }
            else
            {
                if (App.IsInternetAvailable)
                {
                    if (this.Frame != null)
                    {
                        s = text;
                        this.Frame.Navigate(typeof(ItemsPage1), s);
                    }
                }
                else { pop("No internet connection found."); }
            }
        }
        }
        private void adDuplexAd_AdLoadingError(object sender, AdDuplex.WinRT.Models.AdLoadingErrorEventArgs e)
        {
            //adDuplexAd.Visibility = Visibility.Collapsed;
           // adDuplexAd2.Visibility = Visibility.Collapsed;
        }
        private void adDuplexAd2_AdLoaded(object sender, AdDuplex.WinRT.Models.AdLoadedEventArgs e)
        {
            adDuplexAd.Visibility = Visibility.Visible;
            adDuplexAd2.Visibility = Visibility.Visible;
        }
        private void Search_QuerySubmitted(SearchBox sender, SearchBoxQuerySubmittedEventArgs args)
        {
            String text = Enter.QueryText;
            text = text.Replace("-", "");
            Enter.QueryText = text;
            if (text.Equals("")) { ShowPopupAnimationClicked("OOPS :( :( :(\nSearch by title, author, publisher or ISBN of the book."); return; }
            if (check())
            {
                if (App.IsInternetAvailable)
                {
                    if (this.Frame != null)
                    {
                        s = text;
                        this.Frame.Navigate(typeof(SplitPage1), s);
                    }
                }
                else { pop("No internet connection found."); }
            }
            else
            {
                if (App.IsInternetAvailable)
                {
                    if (this.Frame != null)
                    {
                        s = text;
                        this.Frame.Navigate(typeof(ItemsPage1), s);
                    }
                }
                else { pop("No internet connection found."); }
            }
        }
        
    }
}
