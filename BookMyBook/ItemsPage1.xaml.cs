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
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Items Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234233

namespace BookMyBook
{
    /// <summary>
    /// A page that displays a collection of item previews.  In the Split Application this page
    /// is used to display and select one of the available groups.
    /// </summary>
    public sealed partial class ItemsPage1 : Page
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

        public ItemsPage1()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
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
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
            // TODO: Assign a bindable collection of items to this.DefaultViewModel["Items"]
        }
        List<Items> list = new List<Items>();
        String isb = "";

        public async void read()
        {
            progress.IsActive = true;
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
                            catch (Exception ) { }//Message.Show(e.ToString(), "1"+k); }
                            try
                            {
                                d1 = arr.GetObjectAt(k).GetNamedObject("property2").GetNamedString("src");
                                ob.Description = d1;
                                ob.Image = new BitmapImage(new Uri(d1, UriKind.Absolute));
                            }
                            catch (Exception ) { }//Message.Show(e.ToString(), "2"+k); }
                            try
                            {
                                d1 = arr.GetObjectAt(k).GetNamedObject("property3").GetNamedString("text");
                                ob.Subtitle = d1;
                            }
                            catch (Exception ) { }//Message.Show(e.ToString(), "3"+k); }
                            try
                            {
                                d1 = arr.GetObjectAt(k).GetNamedString("prices");
                                d1 = d1.Substring(4);
                                ob.Price = d1;
                            }
                            catch (Exception ) { }//Message.Show(e.ToString(), "3"+k); }
                            list.Add(ob);
                        } 
                        
                        itemGridView.ItemsSource = list;
                        itemGridView.Visibility = Visibility.Visible;
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
            else { itemGridView.ItemsSource = list; itemGridView.Visibility = Visibility.Visible; } progress.IsActive = false;

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
            isb = MainPage.s;
            //isb = e.Parameter.ToString();//Message.Show(isb, "");
            read();
            navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }

        #endregion
        public static Items ob = new Items();
        private void itemGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            
            ob = (Items)e.ClickedItem;
                if (!progress.IsActive)
                {
                    String link = ob.Link;
                    if (!link.Equals(""))
                    {
                        if (this.Frame != null)
                        {

                            this.Frame.Navigate(typeof(SplitPage1), link);
                        }
                    }
                }
        }
       
    }
}
