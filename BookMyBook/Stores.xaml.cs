using BookMyBook.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Hub Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=321224

namespace BookMyBook
{
    /// <summary>
    /// A page that displays a grouped collection of items.
    /// </summary>
    public sealed partial class Stores : Page
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
        public static String s1 = "", s2 = "", s3 = "", s4 = "", s5 = "", s6 = "", s7 = "", s8 = "", h1 = "", h2 = "", h3 = "", h4 = "", h5 = "", h6 = "", h7 = "", h8 = "";
        String ImageUrl = "", title = ""; Boolean imageSet = false, titleSet = false;
        byte price = 0;
        String isb = "";
        int low = 0;
       
        public Stores()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
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
        public async void details()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(new Uri("http://www.kimonolabs.com/api/e22e43lk?apikey=5fc76c6f79ccce5bf6badad02189247e&q=" + isb));
                var jstring = await response.Content.ReadAsStringAsync();
                JsonValue jsonList = JsonValue.Parse(jstring);
                if (jsonList.GetObject().GetNamedString("lastrunstatus").Equals("success"))
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
            }
            catch (Exception e) { Details.Text = "Not Available"; }
        }
        public async void summary()
        {
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(new Uri("http://www.kimonolabs.com/api/e22e43lk?apikey=5fc76c6f79ccce5bf6badad02189247e&q=" + isb));
                var jstring = await response.Content.ReadAsStringAsync();
                JsonValue jsonList = JsonValue.Parse(jstring);
                if (jsonList.GetObject().GetNamedString("lastrunstatus").Equals("success"))
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
            }
            catch (Exception e) { Summary.Text = "Not Available"; }
        }
        public async void Flipkart()
        {
            h1 = "http://www.flipkart.com/books/pr?q=" + isb + "&affid=srujanjha";
            try
            {
                var client = new HttpClient();
                var response = await client.GetAsync(new Uri("http://www.kimonolabs.com/api/e22e43lk?apikey=5fc76c6f79ccce5bf6badad02189247e&q=" + isb));
                var jstring = await response.Content.ReadAsStringAsync();
                JsonValue jsonList = JsonValue.Parse(jstring);
                if (jsonList.GetObject().GetNamedString("lastrunstatus").Equals("success"))
                {
                    s1 = (jsonList.GetObject().GetNamedObject("results").GetObject().GetNamedArray("collection1").GetObjectAt(0).GetNamedString("Price")).Substring(4).Replace(",", "");

                    if (Convert.ToInt16(s1) < low || low == 0)
                    {
                        low = Convert.ToInt16(s1);
                    }
                    ls1.Content = s1; ab1.Visibility = Visibility.Collapsed;
                    ls1.NavigateUri = new Uri(h1);
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
                    s1 = "N/A"; ls1.Content = s1; ab1.Visibility = Visibility.Visible;

                }
            }
            catch (Exception e) { s1 = "N/A"; ls1.Content = s1; ab1.Visibility = Visibility.Visible; }
            price++; n1.IsTapEnabled = true;
            if (price > 6) { Progress.Visibility = Visibility.Collapsed; highlight(); }
            if (imageSet && titleSet) { UpdateTile(); }
            //itemSubtitle.Text = "If the loading is complete and the price still showsN/A, do click on refresh.";
        }
        public async void Infibeam()
        {
            h2 = "http://www.infibeam.com/search.jsp?storeName=Books&query=" + isb + "?track=sruj";
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
                    ls2.Content = s2; ab2.Visibility = Visibility.Collapsed;
                    ls2.NavigateUri = new Uri(h2);
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
                    s2 = "N/A"; ls2.Content = s2; ab2.Visibility = Visibility.Visible;
                }
            }
            catch (Exception e) { s2 = "N/A"; ls2.Content = s2; ab2.Visibility = Visibility.Visible; }
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
                    ls3.Content = s3; ab3.Visibility = Visibility.Collapsed;
                    ls3.NavigateUri = new Uri(h3);
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
                    s3 = "N/A"; ls3.Content = s3; ab3.Visibility = Visibility.Visible;
                }
            }
            catch (Exception e) { s3 = "N/A"; ls3.Content = s3; ab3.Visibility = Visibility.Visible; }
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
                    ls4.Content = s4; ab4.Visibility = Visibility.Collapsed;
                    ls4.NavigateUri = new Uri(h4);
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
                    s4 = "N/A"; ls4.Content = s4; ab4.Visibility = Visibility.Visible;
                }
            }
            catch (Exception e) { s4 = "N/A"; ls4.Content = s4; ab4.Visibility = Visibility.Visible; }
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
                    if (Convert.ToInt16(s5) < low || low == 0)
                    {
                        low = Convert.ToInt16(s5);
                    }
                    ls5.Content = s5; ab5.Visibility = Visibility.Collapsed;
                    ls5.NavigateUri = new Uri(h5);
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
                    s5 = "N/A"; ls5.Content = s5; ab5.Visibility = Visibility.Visible;
                }
            }
            catch (Exception e) { s5 = "N/A"; ls5.Content = s5; ab5.Visibility = Visibility.Visible; }
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
                    ls6.Content = s6; ab6.Visibility = Visibility.Collapsed;
                    ls6.NavigateUri = new Uri(h6);
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
                    s6 = "N/A"; ls6.Content = s6; ab6.Visibility = Visibility.Visible;
                }
            }
            catch (Exception e) { s6 = "N/A"; ls6.Content = s6; ab6.Visibility = Visibility.Visible; }
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
                    ls7.Content = s7; ab7.Visibility = Visibility.Collapsed;
                    ls7.NavigateUri = new Uri(h7); try
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
                    ls7.Content = s7; ab7.Visibility = Visibility.Visible;
                }
            }
            catch (Exception e)
            {
                s7 = "N/A";
                ls7.Content = s7; ab7.Visibility = Visibility.Visible;
            }
            price++; n7.IsTapEnabled = true; if (imageSet && titleSet) { UpdateTile(); }
            if (price > 6) { Progress.Visibility = Visibility.Collapsed; highlight(); }
        }
        public static String uri;
        private void r1_Click(object sender, RoutedEventArgs e) { price--; Progress.Visibility = Visibility.Visible; Flipkart(); }
        private void r2_Click(object sender, RoutedEventArgs e) { price--; Progress.Visibility = Visibility.Visible; Infibeam(); }
        private void r3_Click(object sender, RoutedEventArgs e) { price--; Progress.Visibility = Visibility.Visible; Homeshop18(); }
        private void r4_Click(object sender, RoutedEventArgs e) { price--; Progress.Visibility = Visibility.Visible; Amazon(); }
        private void r5_Click(object sender, RoutedEventArgs e) { price--; Progress.Visibility = Visibility.Visible; Crossword(); }
        private void r6_Click(object sender, RoutedEventArgs e) { price--; Progress.Visibility = Visibility.Visible; uRead(); }
        private void r7_Click(object sender, RoutedEventArgs e) { price--; Progress.Visibility = Visibility.Visible; Landmark(); }

        private async void Image_Tapped_1(object sender, TappedRoutedEventArgs e) { await Launcher.LaunchUriAsync(new Uri(h1)); }
        private async void Image_Tapped_2(object sender, TappedRoutedEventArgs e) { await Launcher.LaunchUriAsync(new Uri(h2)); }
        private async void Image_Tapped_3(object sender, TappedRoutedEventArgs e) { await Launcher.LaunchUriAsync(new Uri(h3)); }
        private async void Image_Tapped_4(object sender, TappedRoutedEventArgs e) { await Launcher.LaunchUriAsync(new Uri(h4)); }
        private async void Image_Tapped_5(object sender, TappedRoutedEventArgs e) { await Launcher.LaunchUriAsync(new Uri(h5)); }
        private async void Image_Tapped_6(object sender, TappedRoutedEventArgs e) { await Launcher.LaunchUriAsync(new Uri(h6)); }
        private async void Image_Tapped_7(object sender, TappedRoutedEventArgs e) { await Launcher.LaunchUriAsync(new Uri(h7)); }
       /* private void App_Resuming(Object sender, Object e)
        {
            Progress.Visibility = Visibility.Visible;
            no = 0;
            if (IsInternet())
            {
                if (MainPage.ct2) { Flipkart(); pr1.IsActive = true; no++; }
                if (MainPage.ct3) { Infibeam(); pr2.IsActive = true; no++; }
                if (MainPage.ct4) { Homeshop18(); pr3.IsActive = true; no++; }
                if (MainPage.ct5) { Amazon(); pr4.IsActive = true; no++; }
                if (MainPage.ct6) { Crossword(); pr5.IsActive = true; no++; }
                if (MainPage.ct7) { uRead(); pr6.IsActive = true; no++; }
                if (MainPage.ct8) { Landmark(); pr7.IsActive = true; no++; }

            }
            else
            {
                p1.Text = "N/A";
                p2.Text = "N/A";
                p3.Text = "N/A";
                p4.Text = "N/A";
                p5.Text = "N/A";
                p6.Text = "N/A";
                p7.Text = "N/A";
                Image.Source = ImageFromRelativePath(this, "Assets/Not_available_icon.jpg");
                Progress.Visibility = Visibility.Collapsed;

            }
        }
        */
        private void UpdateTile()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
            ITileSquare310x310Image tileContent = TileContentFactory.CreateTileSquare310x310Image();
            tileContent.AddImageQuery = true;
            tileContent.Image.Src = ImageUrl;
            tileContent.Image.Alt = "Web Image";
            ITileWide310x150ImageAndText01 wide310x150Content = TileContentFactory.CreateTileWide310x150ImageAndText01();
            wide310x150Content.TextCaptionWrap.Text = "Last Book:ISBN-" + isb;
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
            square310x310TileContent.TextHeadingWrap.Text = Title.Text + Environment.NewLine + "ISBN-" + isb;
            ITileWide310x150Text03 wide310x150TileContent = TileContentFactory.CreateTileWide310x150Text03();
            wide310x150TileContent.TextHeadingWrap.Text = Title.Text + Environment.NewLine + "ISBN-" + isb;
            ITileSquare150x150Text04 square150x150TileContent = TileContentFactory.CreateTileSquare150x150Text04();
            square150x150TileContent.TextBodyWrap.Text = Title.Text + Environment.NewLine + "ISBN-" + isb;
            wide310x150TileContent.Square150x150Content = square150x150TileContent;
            square310x310TileContent.Wide310x150Content = wide310x150TileContent;
            tileNotification = square310x310TileContent.CreateNotification();
            tag = "Title";
            tileNotification.Tag = tag;
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
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
            // TODO: Assign a collection of bindable groups to this.DefaultViewModel["Groups"]
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

    }
}
