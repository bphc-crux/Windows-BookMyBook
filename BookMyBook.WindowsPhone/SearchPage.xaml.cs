using BookMyBook.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace BookMyBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchPage : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();

        public SearchPage()
        {
            this.InitializeComponent();

            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
        }

        /// <summary>
        /// Gets the <see cref="NavigationHelper"/> associated with this <see cref="Page"/>.
        /// </summary>
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }

        /// <summary>
        /// Gets the view model for this <see cref="Page"/>.
        /// This can be changed to a strongly typed view model.
        /// </summary>
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }

        /// <summary>
        /// Populates the page with content passed during navigation.  Any saved state is also
        /// provided when recreating a page from a prior session.
        /// </summary>
        /// <param name="sender">
        /// The source of the event; typically <see cref="NavigationHelper"/>
        /// </param>
        /// <param name="e">Event data that provides both the navigation parameter passed to
        /// <see cref="Frame.Navigate(Type, Object)"/> when this page was initially requested and
        /// a dictionary of state preserved by this page during an earlier
        /// session.  The state will be null the first time a page is visited.</param>
        private void NavigationHelper_LoadState(object sender, LoadStateEventArgs e)
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
        private void NavigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }

        #region NavigationHelper registration

        /// <summary>
        /// The methods provided in this section are simply used to allow
        /// NavigationHelper to respond to the page's navigation methods.
        /// <para>
        /// Page specific logic should be placed in event handlers for the  
        /// <see cref="NavigationHelper.LoadState"/>
        /// and <see cref="NavigationHelper.SaveState"/>.
        /// The navigation parameter is available in the LoadState method 
        /// in addition to page state preserved during an earlier session.
        /// </para>
        /// </summary>
        /// <param name="e">Provides data for navigation methods and event
        /// handlers that cannot cancel the navigation request.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            isb = e.Parameter.ToString();//Message.Show(isb, "");
            read(); 
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        List<Items> list = new List<Items>();
        String isb = "";
        
        public async void read()
        {
            progress.IsActive=true;
            if (list.Count < 2)
            {
                try
                {
                    var client = new HttpClient();
                    var response = await client.GetAsync(new Uri("http://www.kimonolabs.com/api/39p68xv0?apikey=5fc76c6f79ccce5bf6badad02189247e&q=" + isb.Replace(" ", "+")));
                    var jstring = await response.Content.ReadAsStringAsync();
                    JsonValue jsonList = JsonValue.Parse(jstring);
                    if (jsonList.GetObject().GetNamedString("lastrunstatus").Equals("success"))
                    {

                        //  Message.Show(count+"", "");
                        JsonArray arr = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1");
                        int count = arr.Count;
                        String d1 = "";
                        for (uint k = 0; k < count; k++)
                        {
                            Items ob = new Items();
                            try
                            {
                                d1 = arr.GetObjectAt(k).GetNamedObject("property1").GetNamedString("text");
                                ob.Title = d1;
                            }
                            catch (Exception) { }//Message.Show(e.ToString(), "1"+k); }
                            try
                            {
                                d1 = arr.GetObjectAt(k).GetNamedObject("property1").GetNamedString("href");
                                ob.Link = d1;
                            }
                            catch (Exception) { }//Message.Show(e.ToString(), "1"+k); }
                            try
                            {
                                d1 = arr.GetObjectAt(k).GetNamedObject("property2").GetNamedString("src");
                                ob.Description = d1;
                                ob.Image = new BitmapImage(new Uri(d1, UriKind.Absolute));
                            }
                            catch (Exception) { }//Message.Show(e.ToString(), "2"+k); }
                            try
                            {
                                d1 = arr.GetObjectAt(k).GetNamedObject("property3").GetNamedString("text");
                                ob.Subtitle = d1;
                            }
                            catch (Exception) { }//Message.Show(e.ToString(), "3"+k); }
                            try
                            {
                                d1 = arr.GetObjectAt(k).GetNamedString("prices");
                                d1 = d1.Substring(4);
                                ob.Price = d1;
                            }
                            catch (Exception) { }//Message.Show(e.ToString(), "3"+k); }
                            list.Add(ob);
                        } itemListView.ItemsSource = list;
                        itemListView.Visibility = Visibility.Visible;
                    }
                    else
                    {
                        progress.IsActive = false;
                    }
                }
                catch (Exception)
                {
                    progress.IsActive = false;
                }// Message.Show(e.ToString(), isb.Replace(" ", "+")); }
            }
            else { itemListView.ItemsSource = list; itemListView.Visibility = Visibility.Visible; } progress.IsActive = false;
            
        }
        private void refresh_all(object sender, RoutedEventArgs e)
        {
            read();
        }
        public static Items ob = new Items();
        private void itemListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            ob = (Items)e.ClickedItem; 
            if (!progress.IsActive)
            {
                String link = ob.Link;
                if (!link.Equals(""))
                {
                    if (this.Frame != null)
                    {
                        this.Frame.Navigate(typeof(Stores), link);
                    }
                }
            }
        }

        private void itemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ob = (Items)itemListView.SelectedItem; 
            if (!progress.IsActive)
            {
                String link = ob.Link;
                if (!link.Equals(""))
                {
                    if (this.Frame != null)
                    {
                        this.Frame.Navigate(typeof(Stores), link);
                    }
                }
            }
        }
    }
}