using BookMyBook.Common;
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
using System.Net;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;
using System.Net.Http;
using Windows.Networking.Connectivity;

// The Basic Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234237

namespace BookMyBook
{
    /// <summary>
    /// A basic page that provides characteristics common to most applications.
    /// </summary>
    public sealed partial class BasicPage2 : Page
    {
        public static String s1 = "", s2 = "", s3 = "", s4 = "", s5 = "", s6 = "", s7 = "", s8 = "",h1 = "", h2 = "", h3 = "", h4 = "", h5 = "", h6 = "", h7 = "", h8 = "";
        public static int link = 0;
        public static String linkw;
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

        int list = 0,no=0;
        public BasicPage2()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            Progress.Visibility = Visibility.Visible;
            Progress1.Visibility = Visibility.Visible;
            no = 0;
            if (IsInternet())
            {
                if (MainPage.ct2) { Flipkart(); no++; }
                if (MainPage.ct3) { Infibeam(); no++; }
                if (MainPage.ct4) { Homeshop18(); no++; }
                if (MainPage.ct5) { Amazon(); no++; }
                if (MainPage.ct6) { Crossword(); no++; }
                if (MainPage.ct7) { uRead(); no++; }
                if (MainPage.ct8) { Landmark(); no++; }
                Image_update();
            }
            else
            {
                listBox1.Items.Add("Flipkart"); listBox2.Items.Add("N/A");
                listBox1.Items.Add("Infibeam"); listBox2.Items.Add("N/A");
                listBox1.Items.Add("Homeshop18"); listBox2.Items.Add("N/A");
                listBox1.Items.Add("Amazon"); listBox2.Items.Add("N/A");
                listBox1.Items.Add("uRead"); listBox2.Items.Add("N/A");
                listBox1.Items.Add("Crossword"); listBox2.Items.Add("N/A");
                listBox1.Items.Add("Landmark"); listBox2.Items.Add("N/A");
                Image.Source = ImageFromRelativePath(this, "Assets/Not_available_icon.jpg");
                Progress.Visibility = Visibility.Collapsed;
                Progress1.Visibility = Visibility.Collapsed;

            }
           // BookAdda();
            listBox1.FontSize = 30;
            listBox1.SelectionChanged += ListBox1_SelectionChanged;
            //stackPanel1.Children.Add(listBox1);
            listBox2.FontSize = 30;
            listBox2.SelectionChanged += ListBox2_SelectionChanged;
            //stackPanel2.Children.Add(listBox2);
            
        }
        public static bool IsInternet()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;
            return internet;
        }
        private int convert(String s)
        {
            int n = 0;
            while (true)
            {
                try
                {
                    if (s.Contains("."))
                    {
                        return (int)Convert.ToDouble(s);
                    }
                    n = Convert.ToInt16(s);
                    break;
                }
                catch (Exception)
                {
                    if (s.Contains(","))
                        s = s.Remove(s.IndexOf(","), 1);
                    
                }
            }
            return n;
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
        private void rearrange()
        {
            /*try
            {
                int x = 0;
                int[] q = new int[listBox2.Items.Count];
                for (int i = 0; i < listBox2.Items.Count; i++)
                {
                    String s = listBox2.Items.ElementAt(i).ToString();
                    if (s.Equals("N/A")) { }
                    else q[x++] = Convert.ToInt16(s);
                }
                if (x > 0) x--;
                int[] w = new int[7];
                for (int i = 0; i < x; i++) w[i] = q[i];
                String[] st = new String[7];
                Array.Sort(q);
                listBox2.Items.Clear();
                for (int i = 0; i < x; i++)
                {
                    listBox2.Items.Add(q[i]);
                    // listBox2.Items.Add((Array.IndexOf(w, q[i])).ToString());
                    st[i] = listBox1.Items.ElementAt(Array.IndexOf(w, q[i])).ToString();
                    w[Array.IndexOf(w, q[i])] = 0;

                }
                for (int i = x; i < 7; i++)
                {
                    listBox2.Items.Add("N/A");
                }
                for (int i = 0; i < 7; i++)
                    if (w[i] != 0) st[x++] = listBox1.Items.ElementAt(i).ToString();
                listBox1.Items.Clear();
                for (int i = 0; i < st.Length; i++)
                { listBox1.Items.Add(st[i]); }
            }
            catch (Exception) { }*/
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
        public async void Flipkart()
        {
            h1 = "http://www.flipkart.com/books/pr?q=" + MainPage.s;
            int price=0;
            try{
                StringBuilder sb = new StringBuilder();
            byte[] buf = new byte[8192];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h1);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;

            do
            {
                // fill the buffer with data
                count = resStream.Read(buf, 0, buf.Length);

                // make sure we read some data
                if (count != 0)
                {
                    // translate from bytes to ASCII text
                    tempString = Encoding.UTF8.GetString(buf, 0, count);
                    if (tempString.Contains("<meta itemprop=\"price\" content="))
                    {
                        s1 = (tempString.Substring(tempString.IndexOf("<meta itemprop=\"price\" content=") + 32, 10));
                        int l = s1.Length;
                        while (true)
                        {
                            try
                            {
                                price = Convert.ToInt16(s1);
                                s1 = price.ToString();
                                break;
                            }
                            catch (Exception)
                            {
                                l--;
                                s1 = s1.Substring(0, l);
                            }
                        }
                        break;
                    }
                }
            }
            while (count > 0); // any more data to read?
            }
            catch (Exception)
            {
                s1 = "N/A";
            }
            if(!s1.Equals("N/A"))
                listBox2.Items.Add(convert(s1));
            else listBox2.Items.Add(s1);
            listBox1.Items.Add("Flipkart");
            list++;
            if (list >= no) { Progress.Visibility = Visibility.Collapsed; rearrange();}
        }
        public async void Infibeam()
        {
            h2 = "http://www.infibeam.com/search.jsp?storeName=Books&query=" + MainPage.s;
           try{
               byte[] buf = new byte[8192];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h2);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;
            do
            {
                count = resStream.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    tempString = Encoding.UTF8.GetString(buf, 0, count);
                    if (tempString.Contains("infiPrice amount price"))
                    {
                        s2 = (tempString.Substring(tempString.IndexOf("infiPrice amount price") + 24, 10));
                        int price = 0, l = s2.Length;
                        while (true)
                        {
                            try
                            {
                                price = Convert.ToInt16(s2.Substring(l - 1, 1));
                                break;
                            }
                            catch (Exception)
                            {
                                l--;
                                if (l <= 0) { s2 = "N/A"; break; }
                                s2 = s2.Substring(0, l);
                            }
                        }
                        break;
                    }
                }
            }
            while (count > 0); // any more data to read?
            }catch (Exception)
        {
            s2 = "N/A";}
           if (!s2.Equals("N/A"))
               listBox2.Items.Add(convert(s2));
           else listBox2.Items.Add(s2);
            
            listBox1.Items.Add("Infibeam");
            list++;
            if (list >= no) { Progress.Visibility = Visibility.Collapsed; rearrange();}
        }
        public async void Homeshop18()
        {
            h3 = "http://www.homeshop18.com/" + MainPage.s + "/search:" + MainPage.s + "/categoryid:10000/";
            try{
                byte[] buf = new byte[8192];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h3);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;
            do
            {
                count = resStream.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    tempString = Encoding.UTF8.GetString(buf, 0, count);
                    if (tempString.Contains("hs18Price"))
                    {
                        tempString = tempString.Substring(tempString.IndexOf("hs18Price"));
                        s3 = (tempString.Substring(tempString.IndexOf("</span>") + 13, 10));
                        int price = 0, l = s3.Length;
                        while (true)
                        {
                            try
                            {
                                price = Convert.ToInt16(s3.Substring(l - 1, 1));
                                break;
                            }
                            catch (Exception)
                            {
                                l--;
                                if (l <= 0) { s3 = "N/A"; break; }
                                s3 = s3.Substring(0, l);
                            }
                        }
                        break;
                    }
                }
            }
            while (count > 0); // any more data to read?
            }catch (Exception)
        {
            s3 = "N/A";}
            listBox1.Items.Add("Homeshop18");
            if (!s3.Equals("N/A"))
                listBox2.Items.Add(convert(s3));
            else listBox2.Items.Add(s3);
            list++;
            if (list >= no) { Progress.Visibility = Visibility.Collapsed; rearrange();}
        }
        public async void Amazon()
        {
            h4 = "http://www.amazon.in/gp/search/ref=sr_adv_b/?page_nav_name=Books&unfiltered=1&search-alias=stripbooks&field-title=&field-author=&field-keywords=&field-isbn=" + MainPage.s + "&field-publisher=&node=&field-binding_browse-bin=&field-feature_browse-bin=&field-dateop=before&field-dateyear=2014&field-datemod=0&field-price=&sort=relevance-rank&Adv-Srch-Books-Submit.x=31&Adv-Srch-Books-Submit.y=11";
            try{
                byte[] buf = new byte[8192];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h4);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;
            do
            {
                count = resStream.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    tempString = Encoding.UTF8.GetString(buf, 0, count);
                    if (tempString.Contains("bld lrg red"))
                    {
                        s4 = (tempString.Substring(tempString.IndexOf("bld lrg red") + 59, 10));
                        int price = 0, l = s4.Length;
                        while (true)
                        {
                            try
                            {
                                price = Convert.ToInt16(s4.Substring(l - 1, 1));
                                break;
                            }
                            catch (Exception)
                            {
                                l--;
                                if (l <= 0) { s4 = "N/A"; break; }
                                s4 = s4.Substring(0, l);
                            }
                        }
                        break;
                    }
                }
            }
            while (count > 0); // any more data to read?
        }catch (Exception)
        {
            s4 = "N/A";}
            listBox1.Items.Add("Amazon");
            if (!s4.Equals("N/A"))
                listBox2.Items.Add(convert(s4));
            else listBox2.Items.Add(s4);
            list++;
            if (list >= no) { Progress.Visibility = Visibility.Collapsed; rearrange();}
        }
        public async void Crossword()
        {
            h5 = "http://www.crossword.in/home/search?q=" + MainPage.s;
            try{
                byte[] buf = new byte[8192];
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h5);
            HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
            Stream resStream = response.GetResponseStream();
            string tempString = null;
            int count = 0;
            do
            {
                count = resStream.Read(buf, 0, buf.Length);
                if (count != 0)
                {
                    tempString = Encoding.UTF8.GetString(buf, 0, count);
                    if (tempString.Contains("variant-final-price"))
                    {
                        s5 = (tempString.Substring(tempString.IndexOf("variant-final-price") + 75, 10));
                        int price = 0, l = s5.Length;
                        while (true)
                        {
                            try
                            {
                                price = Convert.ToInt16(s5.Substring(l - 1, 1));
                                break;
                            }
                            catch (Exception)
                            {
                                l--;
                                if (l <= 0) { s5 = "N/A"; break; }
                                s5 = s5.Substring(0, l);
                            }
                        }
                        break;
                    }
                }
            }
            while (count > 0); // any more data to read?
            }
            catch (Exception)
            {
                s5 = "N/A";
            }
            listBox1.Items.Add("Crossword");
            if (!s5.Equals("N/A"))
                listBox2.Items.Add(convert(s5));
            else listBox2.Items.Add(s5);
            list++;
            if (list >= no) { Progress.Visibility = Visibility.Collapsed; rearrange();}
        }
        public async void uRead()
        {
            h6 = "http://www.uread.com/search-books/" + MainPage.s;
            try
            {
                byte[] buf = new byte[8192];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h6);
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                Stream resStream = response.GetResponseStream();
                string tempString = null;
                int count = 0;
                do
                {
                    count = resStream.Read(buf, 0, buf.Length);
                    if (count != 0)
                    {
                        tempString = Encoding.UTF8.GetString(buf, 0, count);
                        if (tempString.Contains("lblourPrice"))
                        {
                            s6 = (tempString.Substring(tempString.IndexOf("lblourPrice") + 54, 10));
                            int price = 0, l = s6.Length;
                            while (true)
                            {
                                try
                                {
                                    price = Convert.ToInt16(s6.Substring(l - 1, 1));
                                    break;
                                }
                                catch (Exception)
                                {
                                    l--;
                                    if (l <= 0) { s6 = "N/A"; break; }
                                    s6 = s6.Substring(0, l);
                                }
                            }
                            break;
                        }
                    }
                }
                while (count > 0); // any more data to read?
            }
            catch (Exception)
            {
                s6 = "N/A";
            }
            listBox1.Items.Add("uRead");
            if (!s6.Equals("N/A"))
                listBox2.Items.Add(convert(s6));
            else listBox2.Items.Add(s6);
            list++;
            if (list >= no) { Progress.Visibility = Visibility.Collapsed; rearrange();}
        }
        public async void Landmark()
        {
            h7 = "http://www.landmarkonthenet.com/books/" + MainPage.s;
            try
            {
                byte[] buf = new byte[8192];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h7);
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                Stream resStream = response.GetResponseStream();
                string tempString = null;
                int count = 0;
                do
                {
                    count = resStream.Read(buf, 0, buf.Length);
                    if (count != 0)
                    {
                        tempString = Encoding.UTF8.GetString(buf, 0, count);
                        if (tempString.Contains("price-current"))
                        {
                            s7 = (tempString.Substring(tempString.IndexOf("price-current") + 90, 10));
                            int price = 0, l = s7.Length;
                            while (true)
                            {
                                try
                                {
                                    price = Convert.ToInt16(s7.Substring(l - 1, 1));
                                    break;
                                }
                                catch (Exception)
                                {
                                    l--;
                                    if (l <= 0) { s7 = "N/A"; break; }
                                    s7 = s7.Substring(0, l);
                                }
                            }
                            break;
                        }
                    }
                }
                while (count > 0); // any more data to read?
            }
            catch (Exception)
            {
                s7 = "N/A";
            }
            listBox1.Items.Add("Landmark");
            if (!s7.Equals("N/A"))
                listBox2.Items.Add(convert(s7));
            else listBox2.Items.Add(s7);
            list++;
            if (list >= no) { Progress.Visibility = Visibility.Collapsed; rearrange();}
        }
        /*public async void BookAdda()
        {
            h8 = "http://www.bookadda.com/books/" + MainPage.s;
            try
            {
                byte[] buf = new byte[8192];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h8);
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                Stream resStream = response.GetResponseStream();
                string tempString = null;
                int count = 0;
                do
                {
                    count = resStream.Read(buf, 0, buf.Length);
                    if (count != 0)
                    {
                        tempString = Encoding.UTF8.GetString(buf, 0, count);
                        if (tempString.Contains("span itemprop=\"price\""))
                        {
                            s8 = (tempString.Substring(tempString.IndexOf("span itemprop=\"price\"") + 22, 10));
                            int price = 0, l = s8.Length;
                            listBox1.Items.Add(s8);
                            while (true)
                            {
                                try
                                {
                                    price = Convert.ToInt16(s8.Substring(l - 1, 1));
                                    break;
                                }
                                catch (Exception)
                                {
                                    l--;
                                    if (l <= 0) { s8 = "N/A"; break; }
                                    s8 = s8.Substring(0, l);
                                }
                            }
                            break;
                        }
                    }
                }
                while (count > 0); // any more data to read?
            }
            catch (Exception)
            {
                s8 = "error";
            }
            listBox1.Items.Add("BOOKadda");
            listBox2.Items.Add(s8);
            list++;
            if (list >= no) Progress.Visibility = Visibility.Collapsed;
        }
        */
        public async void Image_update()
        {
            h2 = "http://www.infibeam.com/search.jsp?storeName=Books&query=" + MainPage.s;
            try
            {
                byte[] buf = new byte[8192];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h2);
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                Stream resStream = response.GetResponseStream();
                string tempString = null;
                int count = 0;
                do
                {
                    count = resStream.Read(buf, 0, buf.Length);
                    if (count != 0)
                    {
                        tempString = Encoding.UTF8.GetString(buf, 0, count);
                        if (tempString.Contains("img_med inview photo"))
                        {
                            tempString = (tempString.Substring(tempString.IndexOf("img_med inview photo") + 27));
                            tempString = tempString.Substring(0, tempString.IndexOf("\""));
                            Image.Source = new BitmapImage(new Uri(tempString, UriKind.Absolute));
                            break;
                        }
                    }
                }
                while (count > 0); // any more data to read?
            }
            catch (Exception)
            {
                Image.Source = ImageFromRelativePath(this, "Assets/Not_available_icon.jpg");
            }
            Progress1.Visibility = Visibility.Collapsed;
        }
        public static BitmapImage ImageFromRelativePath(FrameworkElement parent, string path)
        {
            var uri = new Uri(parent.BaseUri, path);
            BitmapImage bmp = new BitmapImage();
            bmp.UriSource = uri;
            return bmp;
        }
        private void ListBox1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            listBox2.SelectedIndex = listBox1.SelectedIndex;
            linkw = listBox1.SelectedItem.ToString();
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(BasicPage1));
            }
        }
        private void ListBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBox1.SelectedIndex = listBox2.SelectedIndex;
            linkw = listBox1.SelectedItem.ToString();
            /*if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(BasicPage1));
            }*/
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
        String str = "";
        private void r1_Click(object sender, RoutedEventArgs e)
        {
            list = 6;Progress.Visibility = Visibility.Visible;
            str = listBox1.Items.ElementAt(0).ToString();
            if (str.Equals("Flipkart")) Flipkart();
            if (str.Equals("Infibeam")) Infibeam();
            if (str.Equals("Homeshop18")) Homeshop18();
            if (str.Equals("Amazon")) Amazon();
            if (str.Equals("uRead")) uRead();
            if (str.Equals("Crossword")) Crossword();
            if (str.Equals("Landmark")) Landmark();
        }
        private void r2_Click(object sender, RoutedEventArgs e)
        {
            list = 6;Progress.Visibility = Visibility.Visible;
            str = listBox1.Items.ElementAt(0).ToString();
            if (str.Equals("Flipkart")) Flipkart();
            if (str.Equals("Infibeam")) Infibeam();
            if (str.Equals("Homeshop18")) Homeshop18();
            if (str.Equals("Amazon")) Amazon();
            if (str.Equals("uRead")) uRead();
            if (str.Equals("Crossword")) Crossword();
            if (str.Equals("Landmark")) Landmark();
        }
        private void r3_Click(object sender, RoutedEventArgs e)
        {
            list = 6; Progress.Visibility = Visibility.Visible;
            str = listBox1.Items.ElementAt(0).ToString();
            if (str.Equals("Flipkart")) Flipkart();
            if (str.Equals("Infibeam")) Infibeam();
            if (str.Equals("Homeshop18")) Homeshop18();
            if (str.Equals("Amazon")) Amazon();
            if (str.Equals("uRead")) uRead();
            if (str.Equals("Crossword")) Crossword();
            if (str.Equals("Landmark")) Landmark();
        }
        private void r4_Click(object sender, RoutedEventArgs e)
        {
            list = 6; Progress.Visibility = Visibility.Visible;
            str = listBox1.Items.ElementAt(0).ToString();
            if (str.Equals("Flipkart")) Flipkart();
            if (str.Equals("Infibeam")) Infibeam();
            if (str.Equals("Homeshop18")) Homeshop18();
            if (str.Equals("Amazon")) Amazon();
            if (str.Equals("uRead")) uRead();
            if (str.Equals("Crossword")) Crossword();
            if (str.Equals("Landmark")) Landmark();
        }
        private void r5_Click(object sender, RoutedEventArgs e)
        {
            list = 6; Progress.Visibility = Visibility.Visible;
            str = listBox1.Items.ElementAt(0).ToString();
            if (str.Equals("Flipkart")) Flipkart();
            if (str.Equals("Infibeam")) Infibeam();
            if (str.Equals("Homeshop18")) Homeshop18();
            if (str.Equals("Amazon")) Amazon();
            if (str.Equals("uRead")) uRead();
            if (str.Equals("Crossword")) Crossword();
            if (str.Equals("Landmark")) Landmark();
        }
        private void r6_Click(object sender, RoutedEventArgs e)
        {
            list = 6; Progress.Visibility = Visibility.Visible;
            str = listBox1.Items.ElementAt(0).ToString();
            if (str.Equals("Flipkart")) Flipkart();
            if (str.Equals("Infibeam")) Infibeam();
            if (str.Equals("Homeshop18")) Homeshop18();
            if (str.Equals("Amazon")) Amazon();
            if (str.Equals("uRead")) uRead();
            if (str.Equals("Crossword")) Crossword();
            if (str.Equals("Landmark")) Landmark();
        }
        private void r7_Click(object sender, RoutedEventArgs e)
        {
            list = 6; Progress.Visibility = Visibility.Visible;
            str = listBox1.Items.ElementAt(0).ToString();
            if (str.Equals("Flipkart")) Flipkart();
            if (str.Equals("Infibeam")) Infibeam();
            if (str.Equals("Homeshop18")) Homeshop18();
            if (str.Equals("Amazon")) Amazon();
            if (str.Equals("uRead")) uRead();
            if (str.Equals("Crossword")) Crossword();
            if (str.Equals("Landmark")) Landmark();
        }
        
    }
}
