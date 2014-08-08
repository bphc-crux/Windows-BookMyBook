using BookMyBook.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.DataTransfer;
using Windows.ApplicationModel.Resources.Core;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;


// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace BookMyBook
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class BasicPage1 : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        /// <summary>
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// NavigationHelper is used on each page to aid in navigation and 
        /// process lifetime management
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        
        public BasicPage1()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            Progress.Visibility = Visibility.Visible;
            read();
            DataTransferManager.GetForCurrentView().DataRequested += dataTransferManager_DataRequested;
            Window.Current.SizeChanged += Window_SizeChanged;
            ChangeLanguage();
        }
        String text1 = "We are piling up the books for you...", text2 = "We are arranging the books for you...", text3 = "Books are finally arranged for you to see...";
        public void ChangeLanguage()
        {
            ResourceMap resourceMap;
            ResourceContext rc = new ResourceContext();
            if (MainPage.currentLang == 1) resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Hindi");
            else if (MainPage.currentLang == 2) resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("French");
            else if (MainPage.currentLang == 3) resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Spanish");
            else resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("English");
            PageTitle.Text = resourceMap.GetValue("PageTitle", rc).ValueAsString;
            Back.Content = resourceMap.GetValue("Back", rc).ValueAsString;
            Forward.Content = resourceMap.GetValue("Forward", rc).ValueAsString;
            Main.Content = resourceMap.GetValue("Home", rc).ValueAsString;
            Share.Content = resourceMap.GetValue("Share", rc).ValueAsString;
            text1 = resourceMap.GetValue("Text1", rc).ValueAsString;
            text2 = resourceMap.GetValue("Text2", rc).ValueAsString;
            text3 = resourceMap.GetValue("Text3", rc).ValueAsString;

        }
        private void Window_SizeChanged(object sender, Windows.UI.Core.WindowSizeChangedEventArgs e)
        {
           var rect= Window.Current.Bounds;
           Web.Width = rect.Width-20.0;
           PageTitle.Width = rect.Width - 340.0;
        }

        void Web_NavigationStarting(WebView sender, WebViewNavigationStartingEventArgs args)
        {
            ProgressText.Text = text1;
        }
        void Web_UnviewableContentIdentified(WebView sender, WebViewUnviewableContentIdentifiedEventArgs args)
        {
            ProgressText.Text = text1;
        }

         void Web_ContentLoading(WebView sender, WebViewContentLoadingEventArgs args)
        {
            ProgressText.Text = text2;
        }

         void Web_DOMContentLoaded(WebView sender, WebViewDOMContentLoadedEventArgs args)
        {
            ProgressText.Text = text3;
        }

        void Web_NavigationCompleted(WebView sender, WebViewNavigationCompletedEventArgs args)
        {
            ProgressText.Text = text3;
            Progress.Visibility = Visibility.Collapsed;
            ProgressText.Visibility = Visibility.Collapsed;
            Mean.Visibility = Visibility.Collapsed;
        }
        private void Share_Click(object sender, RoutedEventArgs e)
        {
            DataTransferManager.ShowShareUI();
        }

        /// <summary>
        /// Called when a share is instigated either through the charms bar or the button in the app
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        async void dataTransferManager_DataRequested(DataTransferManager sender, DataRequestedEventArgs args)
        {
            DataRequest request = args.Request;
            //We are going to use an async API to talk to the webview, so get a deferral for the results
            DataRequestDeferral deferral = args.Request.GetDeferral();
            DataPackage dp = await Web.CaptureSelectedContentToDataPackageAsync();

            if (dp != null && dp.GetView().AvailableFormats.Count > 0)
            {
                // Webview has a selection, so we'll share its data package
                dp.Properties.Title = "This is the selection from the webview control";
                request.Data = dp;
            }
            else
            {
                // No selection, so we'll share the url of the webview
                DataPackage myData = new DataPackage();
                myData.SetWebLink(Web.Source);
                myData.Properties.Title = "This is the URI from the webview control";
                myData.Properties.Description = Web.Source.ToString();
                request.Data = myData;
            }
            deferral.Complete();
        }
    
        /// <summary>
        /// Populates the page with content passed during navigation. Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session. The state will be null the first time a page is visited.</param>
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }

        /// <summary>
        /// Preserves state associated with this page in case the application is suspended or the
        /// page is discarded from the navigation cache.  Values must conform to the serialization
        /// requirements of <see cref="SuspensionManager.SessionState"/>.
        /// </summary>
        /// <param name="sender">The source of the event; typically <see cref="NavigationHelper"/></param>
        /// <param name="e">Event data that provides an empty dictionary to be populated with
        /// serializable state.</param>
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// 
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="GridCS.Common.NavigationHelper.LoadState"/>
        /// and <see cref="GridCS.Common.NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void Forward_Click(object sender, RoutedEventArgs e)
        {
            if (Web.CanGoForward) Web.GoForward();
        }
        private void Back_Click(object sender, RoutedEventArgs e)
        {
            if (Web.CanGoBack) Web.GoBack();
        }
        private void Home_Click(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(MainPage));
            }
        }
        private void adDuplexAd_AdLoadingError(object sender, AdDuplex.WinRT.Models.AdLoadingErrorEventArgs e)
        {
            adDuplexAd1.Visibility = Visibility.Collapsed;
        }

       
    }
}
