using BookMyBook.Common;
using NotificationsExtensions.TileContent;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Email;
using Windows.Data.Json;
using Windows.Data.Xml.Dom;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Storage;
using Windows.System;
using Windows.UI.Notifications;
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
    public sealed partial class Stores : Page
    {
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public static String s1 = "", s2 = "", s3 = "", s4 = "", s5 = "", s6 = "", s7 = "", s8 = "", h1 = "", h2 = "", h3 = "", h4 = "", h5 = "", h6 = "", h7 = "", h8 = "";
        public static bool[] higlight = { true, true, true, true, true, true, true }; 
        String ImageUrl = "", title = ""; Boolean imageSet = false, titleSet = false;
        byte price = 0;
        String isb = "";
        int low = 0;
        public Stores()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += this.NavigationHelper_LoadState;
            this.navigationHelper.SaveState += this.NavigationHelper_SaveState;
            
        }
        private void highlight()
        {
            //Message.Show("Lowest Price:" + low, "BookMyBook");
            try
            {
                UpdateTile();
                if (Convert.ToInt16(s1) == low) { ls1.Foreground = new SolidColorBrush(Windows.UI.Colors.GreenYellow); ls1.FontSize = 35; }
                if (Convert.ToInt16(s2) == low) { ls2.Foreground = new SolidColorBrush(Windows.UI.Colors.GreenYellow); ls2.FontSize = 35; }
                if (Convert.ToInt16(s3) == low) { ls3.Foreground = new SolidColorBrush(Windows.UI.Colors.GreenYellow); ls3.FontSize = 35; }
                if (Convert.ToInt16(s4) == low) { ls4.Foreground = new SolidColorBrush(Windows.UI.Colors.GreenYellow); ls4.FontSize = 35; }
                if (Convert.ToInt16(s5) == low) { ls5.Foreground = new SolidColorBrush(Windows.UI.Colors.GreenYellow); ls5.FontSize = 35; }
                if (Convert.ToInt16(s6) == low) { ls6.Foreground = new SolidColorBrush(Windows.UI.Colors.GreenYellow); ls6.FontSize = 35; }
                if (Convert.ToInt16(s7) == low) { ls7.Foreground = new SolidColorBrush(Windows.UI.Colors.GreenYellow); ls7.FontSize = 35; }
               
            }
            catch (Exception) { }
        }
        public async void flip(int i)
        {
            if (i == 3) if (h1.Equals("")) h1 = "http://www.flipkart.com/books/pr?q=" + isb + "&affid=srujanjha";
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(new Uri("http://www.kimonolabs.com/api/e22e43lk?apikey=5fc76c6f79ccce5bf6badad02189247e&q=" + isb));
                var jstring = await response.Content.ReadAsStringAsync();
                JsonValue jsonList = JsonValue.Parse(jstring);
                if (jsonList.GetObject().GetNamedString("lastrunstatus").Equals("success"))
                {
                    if (i == 1)
                    {
                        int count = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection3").Count;
                        String txt = ""; int hg = 1;
                        for (uint k = 0; k < count; k++)
                        {
                            hg++;
                            String d1 = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection3").GetObjectAt(k).GetNamedObject("Detail").GetNamedString("text");
                            if (d1.Equals("Contributors") || d1.Equals("Book Details") || d1.Equals("Dimensions"))
                            { txt = txt + Environment.NewLine; }
                            if (hg % 2 == 0) txt = txt + d1 + ":";
                            else txt = txt + d1 + Environment.NewLine;
                            if (d1.Equals("Contributors") || d1.Equals("Book Details") || d1.Equals("Dimensions"))
                            { txt = txt + Environment.NewLine + Environment.NewLine; hg = 1; }

                        }
                        Details.Text = txt;
                    }
                    else if (i == 2)
                    {
                        int count = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection2").Count;
                        String txt = "";
                        for (uint k = 0; k < count; k++)
                        {
                            String d1 = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection2").GetObjectAt(k).GetNamedString("Summary");
                            d1 = d1.Replace("\\", "");
                            txt = txt + d1 + Environment.NewLine + Environment.NewLine;
                        }
                        Summary.Text = txt; 
                    }
                    else if (i == 3)
                    {
                        s1 = (jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedString("Price")).Substring(4).Replace(",", "");

                        if (Convert.ToInt16(s1) < low || low == 0)
                        {
                            low = Convert.ToInt16(s1);
                        }
                        ls1.Text = s1; ab1.Visibility = Visibility.Collapsed;
                        // ls1.NavigateUri = new Uri(h1);
                        try
                        {
                            if (!imageSet)
                            {
                                ImageUrl = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedObject("Image").GetNamedString("src");
                                Image.Source = new BitmapImage(new Uri(ImageUrl, UriKind.Absolute)); imageSet = true;
                            }
                        }
                        catch (Exception) { }
                        try
                        {
                            if (!(titleSet))
                            {
                                title = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedString("Name");
                                Title.Text = "Title: " + title; titleSet = true;
                            }
                        }
                        catch (Exception) { }
                    }
                }
                else if (i == 3)
                {
                    s1 = "N/A"; ls1.Text = s1; ab1.Visibility = Visibility.Visible;
                }
            }
            catch (Exception)
            {
                if (i == 1) Details.Text = "Not Available";
                else if (i == 2) Summary.Text = "Not Available";
                else if (i == 3) { if (higlight[0]) { Flipkart(); higlight[0] = false; } else { s1 = "N/A"; ls1.Text = s1; ab1.Visibility = Visibility.Visible; } }
            }
            if (i == 3)
            {
                price++; n1.IsTapEnabled = true;
                if (price > 6) { Progress.Visibility = Visibility.Collapsed; highlight(); }
                if (imageSet && titleSet) { UpdateTile(); }
            }
        }
        public void details()
        {
            flip(1);
        }
        public void summary()
        {
            flip(2);
        }
        public void Flipkart()
        {
            flip(3);
        }
        public async void Infibeam()
        {
            h2 = "http://www.infibeam.com/search.jsp?storeName=Books&query=" + isb;
            try
            {
                var client = new HttpClient(); // Add: using System.Net.Http;
                var response = await client.GetAsync(new Uri("http://www.kimonolabs.com/api/8lt8v5pi?apikey=5fc76c6f79ccce5bf6badad02189247e&query=" + isb));
                var jstring = await response.Content.ReadAsStringAsync();
                JsonValue jsonList = JsonValue.Parse(jstring);
                if (jsonList.GetObject().GetNamedString("lastrunstatus").Equals("success"))
                {
                    s2 = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedString("Price").Replace(",", "");
                    if (Convert.ToInt16(s2) < low || low == 0)
                    {
                        low = Convert.ToInt16(s2);
                    }
                    ls2.Text = s2; ab2.Visibility = Visibility.Collapsed;
                    // ls2.NavigateUri = new Uri(h2);
                    try
                    {
                        if (!imageSet)
                        {
                            ImageUrl = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedObject("Image").GetNamedString("src");
                            Image.Source = new BitmapImage(new Uri(ImageUrl, UriKind.Absolute)); imageSet = true;
                        }
                    }
                    catch (Exception) { }
                    try
                    {
                        if (!(titleSet))
                        {
                            title = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedString("Name");
                            Title.Text = "Title: " + title; titleSet = true;
                        }
                    }
                    catch (Exception) { }
                    // Message.Show(h2, "");

                    //itemSubtitle.Text = "We are trying our best to bring you the price sooner.";
                }
                else
                {
                    s2 = "N/A"; ls2.Text = s2; ab2.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ) { s2 = "N/A"; ls2.Text = s2; ab2.Visibility = Visibility.Visible; }
            price++; n2.IsTapEnabled = true; if (imageSet && titleSet) { UpdateTile(); }
            if (price > 6) { Progress.Visibility = Visibility.Collapsed; highlight(); }
        }
        public async void Homeshop18()
        {
            h3 = "http://www.homeshop18.com/" + isb + "/search:" + isb + "/categoryid:10000/";
            try
            {
                var client = new HttpClient(); // Add: using System.Net.Http;
                var response = await client.GetAsync(new Uri("http://www.kimonolabs.com/api/35tl9fa8?apikey=5fc76c6f79ccce5bf6badad02189247e&kimpath1=" + isb + "&kimpath2=search:" + isb));
                var jstring = await response.Content.ReadAsStringAsync();
                JsonValue jsonList = JsonValue.Parse(jstring);
                if (jsonList.GetObject().GetNamedString("lastrunstatus").Equals("success"))
                {
                    s3 = (jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedString("Price")).Substring(2).Replace(",", "");
                    if (Convert.ToInt16(s3) < low || low == 0)
                    {
                        low = Convert.ToInt16(s3);
                    }
                    ls3.Text = s3; ab3.Visibility = Visibility.Collapsed;
                    // ls3.NavigateUri = new Uri(h3);
                    try
                    {
                        if (!imageSet)
                        {
                            ImageUrl = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedObject("Image").GetNamedString("src");
                            Image.Source = new BitmapImage(new Uri(ImageUrl, UriKind.Absolute)); imageSet = true;
                        }
                    }
                    catch (Exception) { }
                    try
                    {
                        if (!(titleSet))
                        {
                            title = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedString("Name");
                            Title.Text = "Title: " + title; titleSet = true;
                        }
                    }
                    catch (Exception) { }
                }
                else
                {
                    s3 = "N/A"; ls3.Text = s3; ab3.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ) { s3 = "N/A"; ls3.Text = s3; ab3.Visibility = Visibility.Visible; }
            price++; n3.IsTapEnabled = true; if (imageSet && titleSet) { UpdateTile(); }
            if (price > 6) { Progress.Visibility = Visibility.Collapsed; highlight(); }
        }
        public async void Amazon()
        {
            h4 = "http://www.amazon.in/gp/search/ref=sr_adv_b/?page_nav_name=Books&unfiltered=1&search-alias=stripbooks&field-title=&field-author=&field-keywords=&field-isbn=" + isb + "&field-publisher=&node=&field-binding_browse-bin=&field-feature_browse-bin=&field-dateop=before&field-dateyear=2014&field-datemod=0&field-price=&sort=relevance-rank&Adv-Srch-Books-Submit.x=31&Adv-Srch-Books-Submit.y=11";
            try
            {
                var client = new HttpClient(); // Add: using System.Net.Http;
                var response = await client.GetAsync(new Uri("http://www.kimonolabs.com/api/4od48j0s?apikey=5fc76c6f79ccce5bf6badad02189247e&field-keywords=" + isb));
                var jstring = await response.Content.ReadAsStringAsync();
                JsonValue jsonList = JsonValue.Parse(jstring);
                if (jsonList.GetObject().GetNamedString("lastrunstatus").Equals("success"))
                {
                    s4 = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedObject("Price").GetNamedString("text").Replace(",", "");
                    h4 = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedObject("Price").GetNamedString("href");

                    double dt = Convert.ToDouble(s4);
                    int pt = Convert.ToInt16(dt);
                    s4 = pt.ToString();
                    if (Convert.ToInt16(s4) < low || low == 0)
                    {
                        low = Convert.ToInt16(s4);
                    }
                    ls4.Text = s4; ab4.Visibility = Visibility.Collapsed;
                    //  ls4.NavigateUri = new Uri(h4);
                    try
                    {
                        if (!imageSet)
                        {
                            ImageUrl = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedObject("Image").GetNamedString("src");
                            Image.Source = new BitmapImage(new Uri(ImageUrl, UriKind.Absolute)); imageSet = true;
                        }

                    }
                    catch (Exception) { }
                    try
                    {
                        if (!(titleSet))
                        {
                            title = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedObject("Name").GetNamedString("text");
                            Title.Text = "Title: " + title; titleSet = true;
                        }
                    }
                    catch (Exception) { }
                }
                else
                {
                    s4 = "N/A"; ls4.Text = s4; ab4.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ) { s4 = "N/A"; ls4.Text = s4; ab4.Visibility = Visibility.Visible; }
            price++; n4.IsTapEnabled = true; if (imageSet && titleSet) { UpdateTile(); }
            if (price > 6) { Progress.Visibility = Visibility.Collapsed; highlight(); }
        }
        public async void Crossword()
        {
            h5 = "http://www.crossword.in/home/search?q=" + isb;
            try
            {
                var client = new HttpClient(); // Add: using System.Net.Http;
                var response = await client.GetAsync(new Uri("http://www.kimonolabs.com/api/5alr92si?apikey=5fc76c6f79ccce5bf6badad02189247e&q=" + isb));
                var jstring = await response.Content.ReadAsStringAsync();
                JsonValue jsonList = JsonValue.Parse(jstring);
                if (jsonList.GetObject().GetNamedString("lastrunstatus").Equals("success"))
                {
                    s5 = (jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedString("Price")).Substring(2).Replace(",", "");
                    h5 = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedObject("Name").GetNamedString("href");
                    if (Convert.ToInt16(s5) < low || low == 0)
                    {
                        low = Convert.ToInt16(s5);
                    }
                    ls5.Text = s5; ab5.Visibility = Visibility.Collapsed;
                    //   ls5.NavigateUri = new Uri(h5);
                    try
                    {
                        if (!imageSet)
                        {
                            ImageUrl = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedObject("Image").GetNamedString("src");
                            Image.Source = new BitmapImage(new Uri(ImageUrl, UriKind.Absolute)); imageSet = true;
                        }
                    }
                    catch (Exception) { }
                    try
                    {
                        if (!(titleSet))
                        {
                            title = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedObject("Name").GetNamedString("text");
                            Title.Text = "Title: " + title; titleSet = true;
                        }

                    }
                    catch (Exception) { }

                }
                else
                {
                    s5 = "N/A"; ls5.Text = s5; ab5.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ) { s5 = "N/A"; ls5.Text = s5; ab5.Visibility = Visibility.Visible; }
            price++; n5.IsTapEnabled = true; if (imageSet && titleSet) { UpdateTile(); }
            if (price > 6) { Progress.Visibility = Visibility.Collapsed; highlight(); }
        }
        public async void uRead()
        {
            h6 = "http://www.uread.com/search-books/" + isb;
            try
            {
                var client = new HttpClient(); // Add: using System.Net.Http;
                var response = await client.GetAsync(new Uri("http://www.kimonolabs.com/api/bs6sqzv0?apikey=5fc76c6f79ccce5bf6badad02189247e&kimpath2=" + isb));
                var jstring = await response.Content.ReadAsStringAsync();
                JsonValue jsonList = JsonValue.Parse(jstring);
                if (jsonList.GetObject().GetNamedString("lastrunstatus").Equals("success"))
                {
                    s6 = (jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedString("Price")).Substring(1).Replace(",", "");
                    if (Convert.ToInt16(s6) < low || low == 0)
                    {
                        low = Convert.ToInt16(s6);
                    }
                    ls6.Text = s6; ab6.Visibility = Visibility.Collapsed;
                    //      ls6.NavigateUri = new Uri(h6);
                    try
                    {
                        if (!imageSet)
                        {
                            ImageUrl = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedObject("Image").GetNamedString("src");
                            Image.Source = new BitmapImage(new Uri(ImageUrl, UriKind.Absolute)); imageSet = true;
                        }

                    }
                    catch (Exception) { }
                    try
                    {
                        if (!(titleSet))
                        {
                            title = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedString("Name");
                            Title.Text = "Title: " + title; titleSet = true;
                        }
                    }
                    catch (Exception) { }
                }
                else
                {
                    s6 = "N/A"; ls6.Text = s6; ab6.Visibility = Visibility.Visible;
                }
            }
            catch (Exception ) { s6 = "N/A"; ls6.Text = s6; ab6.Visibility = Visibility.Visible; }
            price++; n6.IsTapEnabled = true; if (imageSet && titleSet) { UpdateTile(); }
            if (price > 6) { Progress.Visibility = Visibility.Collapsed; highlight(); }
        }
        public async void Landmark()
        {
            h7 = "http://www.landmarkonthenet.com/books/" + isb;
            try
            {
                var client = new HttpClient(); // Add: using System.Net.Http;
                var response = await client.GetAsync(new Uri("http://www.kimonolabs.com/api/7r70l3y6?apikey=5fc76c6f79ccce5bf6badad02189247e&kimpath2=" + isb));
                var jstring = await response.Content.ReadAsStringAsync();
                JsonValue jsonList = JsonValue.Parse(jstring);
                if (jsonList.GetObject().GetNamedString("lastrunstatus").Equals("success"))
                {
                    s7 = (jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedString("Price")).Substring(3).Replace(",", "");
                    if (Convert.ToInt16(s7) < low || low == 0)
                    {
                        low = Convert.ToInt16(s7);
                    }
                    ls7.Text = s7; ab7.Visibility = Visibility.Collapsed;
                    //    ls7.NavigateUri = new Uri(h7);
                    try
                    {
                        if (!imageSet)
                        {
                            ImageUrl = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedObject("Image").GetNamedString("src");
                            Image.Source = new BitmapImage(new Uri(ImageUrl, UriKind.Absolute)); imageSet = true;
                        }
                    }
                    catch (Exception) { }
                    try
                    {
                        if (!(titleSet))
                        {
                            title = jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedString("Name");
                            Title.Text = "Title: " + title; titleSet = true;
                        }
                    }
                    catch (Exception) { }
                }
                else
                {
                    s7 = "N/A";
                    ls7.Text = s7; ab7.Visibility = Visibility.Visible;
                }
            }
            catch (Exception )
            {
                s7 = "N/A";
                ls7.Text = s7; ab7.Visibility = Visibility.Visible;
            }
            price++; n7.IsTapEnabled = true; if (imageSet && titleSet) { UpdateTile(); }
            if (price > 6) { Progress.Visibility = Visibility.Collapsed; highlight(); }
        }
        public static String uri;
        
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
            long ibs = 0L;
            if (!(Int64.TryParse(e.Parameter.ToString(), out ibs)))
            {
                Items ob = new Items();
                isb = (e.Parameter).ToString();
                ob = SearchPage.ob;
                h1 = isb + "&affid=srujanjha";
                isb = isb.Substring(isb.IndexOf("pid") + 4, 13);
                Title.Text = ob.Title;
                Image.Source = ob.Image;
                ImageUrl = ob.Description;
                title = ob.Title;
                s1 = ob.Price;
                try
                {
                    if (Convert.ToInt16(s1) < low || low == 0)
                    {
                        low = Convert.ToInt16(s1);
                    }
                    ls1.Text = s1; ab1.Visibility = Visibility.Collapsed; price++;
                }
                catch (Exception) { Flipkart(); }
                imageSet = true;
                titleSet = true;
            }
            else
            {
                isb = ibs + ""; imageSet = false;
                titleSet = false;
            }
            try { Homeshop18(); Infibeam(); Amazon(); uRead(); Landmark(); Crossword(); details(); summary(); }
            catch (Exception) { }
            this.navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            this.navigationHelper.OnNavigatedFrom(e);
        }

        #endregion

        private void r1_Click(object sender, RoutedEventArgs e) { price--; Progress.Visibility = Visibility.Visible; Flipkart(); }
        private void r2_Click(object sender, RoutedEventArgs e) { price--; Progress.Visibility = Visibility.Visible; Infibeam(); }
        private void r3_Click(object sender, RoutedEventArgs e) { price--; Progress.Visibility = Visibility.Visible; Homeshop18(); }
        private void r4_Click(object sender, RoutedEventArgs e) { price--; Progress.Visibility = Visibility.Visible; Amazon(); }
        private void r5_Click(object sender, RoutedEventArgs e) { price--; Progress.Visibility = Visibility.Visible; Crossword(); }
        private void r6_Click(object sender, RoutedEventArgs e) { price--; Progress.Visibility = Visibility.Visible; uRead(); }
        private void r7_Click(object sender, RoutedEventArgs e) { price--; Progress.Visibility = Visibility.Visible; Landmark(); }
        private async void ListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            switch (ListView1.SelectedIndex)
            {
                case 0: await Launcher.LaunchUriAsync(new Uri(h1)); break;
                case 1: await Launcher.LaunchUriAsync(new Uri(h2)); break;
                case 2: await Launcher.LaunchUriAsync(new Uri(h3)); break;
                case 3: await Launcher.LaunchUriAsync(new Uri(h4)); break;
                case 4: await Launcher.LaunchUriAsync(new Uri(h5)); break;
                case 5: await Launcher.LaunchUriAsync(new Uri(h6)); break;
                case 6: await Launcher.LaunchUriAsync(new Uri(h7)); break;
            }
        }

        private void ListView1_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (((StackPanel)e.ClickedItem).Equals(sp1)) { ListView1.SelectedIndex = 0; }
            if (((StackPanel)e.ClickedItem).Equals(sp2)) { ListView1.SelectedIndex = 1; }
            if (((StackPanel)e.ClickedItem).Equals(sp3)) { ListView1.SelectedIndex = 2; }
            if (((StackPanel)e.ClickedItem).Equals(sp4)) { ListView1.SelectedIndex = 3; }
            if (((StackPanel)e.ClickedItem).Equals(sp5)) { ListView1.SelectedIndex = 4; }
            if (((StackPanel)e.ClickedItem).Equals(sp6)) { ListView1.SelectedIndex = 5; }
            if (((StackPanel)e.ClickedItem).Equals(sp7)) { ListView1.SelectedIndex = 6; }

        }
        
        private void refresh_all(object sender, RoutedEventArgs e)
        {
            price = 0; 
            Flipkart(); Homeshop18(); Infibeam(); Amazon(); uRead(); Landmark(); Crossword();
            Progress.Visibility = Visibility.Visible;
        }
        private void about_event(object sender, RoutedEventArgs e)
        {
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(About));
            }
        }
        private void settings_event(object sender, RoutedEventArgs e)
        {

        }
        private async void Feedback_event(object sender, RoutedEventArgs e)
        {

            EmailRecipient sendTo = new EmailRecipient()
            {
                Address = "srujanjha.jha@gmail.com"
            };

            //generate mail object
            EmailMessage mail = new EmailMessage();
            mail.Subject = "Feedback for BookMyBook";
            mail.Body = "Write your feedback here...";

            //add recipients to the mail object
            mail.To.Add(sendTo);
            //mail.Bcc.Add(sendTo);
            //mail.CC.Add(sendTo);

            //open the share contract with Mail only:
            await EmailManager.ShowComposeNewEmailAsync(mail);
        }
        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            BigTitle.Text = Title.Text;
            BigImage.Source = Image.Source;
            ImageViewer.IsOpen = true;
        }
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            ImageViewer.IsOpen = false;
        }
        private void Title_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Results.SelectedIndex = 1;
        }
        private void UpdateTile()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
            ITileSquare310x310Image tileContent = TileContentFactory.CreateTileSquare310x310Image();
            tileContent.AddImageQuery = true;
            tileContent.Image.Src = ImageUrl;
            tileContent.Image.Alt = "Web Image";
            ITileWide310x150ImageAndText01 wide310x150Content = TileContentFactory.CreateTileWide310x150ImageAndText01();
            wide310x150Content.TextCaptionWrap.Text = "Last Book:ISBN-"+isb;
            wide310x150Content.Image.Src = ImageUrl;
            wide310x150Content.Image.Alt = "Web image";
            ITileSquare150x150Image square150x150Content = TileContentFactory.CreateTileSquare150x150Image();
            square150x150Content.Image.Src = ImageUrl;
            square150x150Content.Image.Alt = "Web image";
            wide310x150Content.Square150x150Content = square150x150Content;
            tileContent.Wide310x150Content = wide310x150Content;
            TileNotification tileNotification = tileContent.CreateNotification();
            string tag = "Image";
            tileNotification.Tag = tag;
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
            ITileSquare310x310Text09 square310x310TileContent = TileContentFactory.CreateTileSquare310x310Text09();
            square310x310TileContent.TextHeadingWrap.Text =  Title.Text + Environment.NewLine + "ISBN-" + isb;
            ITileWide310x150Text03 wide310x150TileContent = TileContentFactory.CreateTileWide310x150Text03();
            wide310x150TileContent.TextHeadingWrap.Text =  Title.Text + Environment.NewLine + "ISBN-" + isb;
            ITileSquare150x150Text04 square150x150TileContent = TileContentFactory.CreateTileSquare150x150Text04();
            square150x150TileContent.TextBodyWrap.Text =  Title.Text + Environment.NewLine + "ISBN-" + isb;
            wide310x150TileContent.Square150x150Content = square150x150TileContent;
            square310x310TileContent.Wide310x150Content = wide310x150TileContent;
            tileNotification = square310x310TileContent.CreateNotification();
            tag = "Title";
            tileNotification.Tag = tag;
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }

        private void refresh_summary(object sender, RoutedEventArgs e)
        {
            summary();
        }

        private void refresh_details(object sender, RoutedEventArgs e)
        {
            details();
        }

        private void refresh_image(object sender, RoutedEventArgs e)
        {
            imageSet = false; titleSet = false;
            try { Flipkart(); Homeshop18(); Infibeam(); Amazon(); uRead(); Landmark(); Crossword();  }
            catch (Exception ) { }
        }
        private void read()
        {
            if (MainPage.save <= 1) {  Tut.IsOpen = true; }
                        else Tut.IsOpen = false;
        }
        private void Tutorial_event(object sender, RoutedEventArgs e)
        {
            if (enable.Label.Equals("enable tutorial"))
            {
                enable.Label = "disable tutorial";
                Tut.IsOpen = true;
            }
            else
            {
                enable.Label = "enable tutorial";
                Tut.IsOpen = false;
            }
        }
        private void Tut_Closed(object sender, object e)
        {
            if (Tut.IsOpen)
            {
                enable.Label = "disable tutorial";
            }
            else
            {
                enable.Label = "enable tutorial";
            }
        }
        private void Tut_Close(object sender, RoutedEventArgs e)
        {
            enable.Label = "enable tutorial"; Tut.IsOpen = false;
        }
        
        private void Tutorial_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Tutorial.SelectedIndex >= 1) BottomAppBar.IsOpen = true;
            }
            catch (Exception) { }
        }
        
    }
}
