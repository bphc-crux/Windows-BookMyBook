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
using Windows.ApplicationModel.Resources.Core;
using NotificationsExtensions.TileContent;
using Windows.UI.Notifications;

namespace BookMyBook
{
   public sealed partial class BasicPage3 : Page
    {
        public static String s1 = "", s2 = "", s3 = "", s4 = "", s5 = "", s6 = "", s7 = "", s8 = "", h1 = "", h2 = "", h3 = "", h4 = "", h5 = "", h6 = "", h7 = "", h8 = "";
        public static int link = 0;
        public static String linkw;
        int high = -1, price = -1;
        private NavigationHelper navigationHelper;
        private ObservableDictionary defaultViewModel = new ObservableDictionary();
        public ObservableDictionary DefaultViewModel
        {
            get { return this.defaultViewModel; }
        }
        int no = 0, list = 0;
        public NavigationHelper NavigationHelper
        {
            get { return this.navigationHelper; }
        }
        public BasicPage3()
        {
            this.InitializeComponent();
            this.navigationHelper = new NavigationHelper(this);
            this.navigationHelper.LoadState += navigationHelper_LoadState;
            this.navigationHelper.SaveState += navigationHelper_SaveState;
            Application.Current.Resuming += new EventHandler<Object>(App_Resuming);
            ProgressText.Text = "Please be patient...";
            Progress.Visibility = Visibility.Visible;
            Progress1.Visibility = Visibility.Visible;
            no = 0;
            UpdateISBN();
            if (IsInternet())
            {
                ProgressText.Text = "You can see the last book searched on the tile of BookMyBook";
                Complete_update();
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
                Progress1.Visibility = Visibility.Collapsed;
            }
            ChangeLanguage();
        }
        public void ChangeLanguage()
        {
            ResourceMap resourceMap;
            if (MainPage.currentLang == 1) resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Hindi");
            else if (MainPage.currentLang == 2) resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("French");
            else if (MainPage.currentLang == 3) resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("Spanish");
            else resourceMap = ResourceManager.Current.MainResourceMap.GetSubtree("English");
            PageTitle.Text = resourceMap.GetValue("PageTitle").ValueAsString;
            tr1.Text = resourceMap.GetValue("Flipkart").ValueAsString;
            tr2.Text = resourceMap.GetValue("Infibeam").ValueAsString;
            tr3.Text = resourceMap.GetValue("Homeshop18").ValueAsString;
            tr4.Text = resourceMap.GetValue("Amazon").ValueAsString;
            tr5.Text = resourceMap.GetValue("Crossword").ValueAsString;
            tr6.Text = resourceMap.GetValue("uRead").ValueAsString;
            tr7.Text = resourceMap.GetValue("Landmark").ValueAsString;
            tx1.Text = resourceMap.GetValue("tx1").ValueAsString;
            tx2.Text = resourceMap.GetValue("tx2").ValueAsString;
            tx3.Text = resourceMap.GetValue("tx3").ValueAsString;
        }
        private void App_Resuming(Object sender, Object e)
        {
            Progress.Visibility = Visibility.Visible;
            Progress1.Visibility = Visibility.Visible;
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
                Image_update();
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
                Progress1.Visibility = Visibility.Collapsed;

            }
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
        private void navigationHelper_LoadState(object sender, LoadStateEventArgs e)
        {
        }
        private void navigationHelper_SaveState(object sender, SaveStateEventArgs e)
        {
        }
        public async void Flipkart()
        {
            h1 = "http://www.flipkart.com/books/pr?q=" + MainPage.s;
            int price = 0;
            try
            {
                StringBuilder sb = new StringBuilder();
                byte[] buf = new byte[8192];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h1);
                HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync();
                //request.ContinueTimeout = 10;
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
            if (!s1.Equals("N/A"))
                p1.Text=convert(s1).ToString();
            else p1.Text=s1;
            list++;
            if (list >= no) { Progress.Visibility = Visibility.Collapsed;  }
            highlight();
            pr1.IsActive = false;
            ProgressText.Text = "If the loading is complete and the price still shows N/A, do click on refresh.";
        }
        public async void Infibeam()
        {
            h2 = "http://www.infibeam.com/search.jsp?storeName=Books&query=" + MainPage.s;
            try
            {
                byte[] buf = new byte[8192];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h2);
                //request.ContinueTimeout = 10;
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
            }
            catch (Exception)
            {
                s2 = "N/A";
            }
            if (!s2.Equals("N/A"))
                p2.Text=convert(s2).ToString();
            else p2.Text = (s2);
            list++; highlight();
            if (list >= no) { Progress.Visibility = Visibility.Collapsed;  }
            pr2.IsActive = false;
            ProgressText.Text = "We are trying our best to bring you the price sooner.";
        }
        public async void Homeshop18()
        {
            h3 = "http://www.homeshop18.com/" + MainPage.s + "/search:" + MainPage.s + "/categoryid:10000/";
            try
            {
                byte[] buf = new byte[8192];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h3);
                //request.ContinueTimeout = 10;
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
            }
            catch (Exception)
            {
                s3 = "N/A";
            }
            if (!s3.Equals("N/A"))
                p3.Text = convert(s3).ToString();
            else p3.Text = "N/A";
            list++; highlight();
            if (list >= no) { Progress.Visibility = Visibility.Collapsed;  }
            pr3.IsActive = false;
            ProgressText.Text = "Internet connection may be slow.";
        }
        public async void Amazon()
        {
            h4 = "http://www.amazon.in/gp/search/ref=sr_adv_b/?page_nav_name=Books&unfiltered=1&search-alias=stripbooks&field-title=&field-author=&field-keywords=&field-isbn=" + MainPage.s + "&field-publisher=&node=&field-binding_browse-bin=&field-feature_browse-bin=&field-dateop=before&field-dateyear=2014&field-datemod=0&field-price=&sort=relevance-rank&Adv-Srch-Books-Submit.x=31&Adv-Srch-Books-Submit.y=11";
            try
            {
                byte[] buf = new byte[8192];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h4);
                //request.ContinueTimeout = 10;
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
            }
            catch (Exception)
            {
                s4 = "N/A";
            }
            if (!s4.Equals("N/A"))
                p4.Text = convert(s4).ToString();
            else p4.Text = "N/A"; list++; highlight();
            if (list >= no) { Progress.Visibility = Visibility.Collapsed;  }
            pr4.IsActive = false;
            ProgressText.Text = "We are trying our best to bring you the price sooner.";
        }
        public async void Crossword()
        {
            h5 = "http://www.crossword.in/home/search?q=" + MainPage.s;
            try
            {
                byte[] buf = new byte[8192];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h5);
               // request.ContinueTimeout = 10;
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
            if (!s5.Equals("N/A"))
                p5.Text = convert(s5).ToString();
            else p5.Text = "N/A"; list++; highlight();
            if (list >= no) { Progress.Visibility = Visibility.Collapsed; }
            pr5.IsActive = false;
            ProgressText.Text = "You can see the last book searched on the tile of BookMyBook";
        }
        public async void uRead()
        {
            h6 = "http://www.uread.com/search-books/" + MainPage.s;
            try
            {
                byte[] buf = new byte[8192];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h6);
               // request.ContinueTimeout = 10;
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
            if (!s6.Equals("N/A"))
                p6.Text = convert(s6).ToString();
            else p6.Text = "N/A"; list++; highlight();
            if (list >= no) { Progress.Visibility = Visibility.Collapsed; }
            pr6.IsActive = false;
            ProgressText.Text = "Please be patient...";
        }
        public async void Landmark()
        {
            h7 = "http://www.landmarkonthenet.com/books/" + MainPage.s;
            try
            {
                byte[] buf = new byte[8192];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h7);
                //request.ContinueTimeout = 10;
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
            if (!s7.Equals("N/A"))
                p7.Text = convert(s7).ToString();
            else p7.Text = "N/A"; list++; highlight();
            if (list >= no) { Progress.Visibility = Visibility.Collapsed;  }
            pr7.IsActive = false;
            ProgressText.Text = "Internet connection may be slow.";
        }
        String ImageUrl = "", Titlen = "";
        public async void Image_update1()
        {
            h1 = "http://www.flipkart.com/books/pr?q=" + MainPage.s;
            try
            {
                byte[] buf = new byte[8192];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h1);
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
                        ProgressText.Text = tempString;
                        if (tempString.Contains("og:image"))
                        {
                            tempString = (tempString.Substring(tempString.IndexOf("og:image") + 19));
                            tempString = tempString.Substring(0, tempString.IndexOf("\"/>"));
                            Image.Source = new BitmapImage(new Uri(tempString, UriKind.Absolute));
                            ImageUrl = tempString;
                            break;
                        }
                    }
                }
                while (count > 0);
            }
            catch (Exception)
            {
                Image.Source = ImageFromRelativePath(this, "Assets/Not_available_icon.jpg");
            }
        }
        public async void Image_update()
        {
            int over = 0;
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
                            ImageUrl = tempString;
                            over = 1;
                            break;
                        }
                    }
                }
                while (count > 0); // any more data to read?
            }
            catch (Exception)
            {
                Image_update1();
                over = 1;
            }
            if (over == 0) Image_update1();
        }
        public async void Title_update1()
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
                        if (tempString.Contains("class=\"fn\""))
                        {
                            Title.Text = tempString;
                            
                            tempString = (tempString.Substring(tempString.IndexOf("class=\"fn\"") + 18));
                            tempString = tempString.Substring(0, tempString.IndexOf("\">")); 
                            tempString = tempString.Trim();
                            Title.Text = "Title:"+tempString;
                            Titlen = tempString;
                            break;
                        }
                    }
                }
                while (count > 0); // any more data to read?
            }
            catch (Exception)
            {
                //Image.Source = ImageFromRelativePath(this, "Assets/Not_available_icon.jpg");
            }
        }
        public async void Title_update()
        {
            int over = 0;
            h1 = "http://www.flipkart.com/books/pr?q=" + MainPage.s;
            try
            {
                byte[] buf = new byte[8192];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h1);
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
                        if (tempString.Contains("itemprop=\"name\""))
                        {
                            //ProgressText.Text = tempString;
                            tempString = (tempString.Substring(tempString.IndexOf("itemprop=\"name\"") + 16));
                            tempString = tempString.Substring(0, tempString.IndexOf("</h1>"));
                            tempString = tempString.Trim();
                            Title.Text = "Title:" + tempString;
                            Titlen = tempString;
                            over = 1;
                            break;
                        }
                    }
                }
                while (count > 0); // any more data to read?
            }
            catch (Exception)
            {
                Title_update1();
                over = 1;
            }
            if (over == 0) Title_update1();
        }
        public async void Complete_update()
        {
            h5 = "http://www.crossword.in/home/search?q=" + MainPage.s;
            try
            {
                byte[] buf = new byte[8192];
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(h5);
               // request.ContinueTimeout = 10;
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
                        if (tempString.Contains("img data"))
                        {
                            String dtitle = tempString.Substring(tempString.IndexOf("img data")+25+MainPage.s.Length);
                            String dimage = dtitle.Substring(dtitle.IndexOf("\"") + 7);
                            dtitle = dtitle.Substring(0, dtitle.IndexOf("\""));
                            dimage = dimage.Substring(0, dimage.IndexOf("\""));
                            Title.Text = "Title:" + dtitle;
                            Image.Source = new BitmapImage(new Uri(dimage, UriKind.Absolute));
                            ImageUrl = dimage;
                            Titlen = dtitle;
                            break;
                        }
                    }
                }
                while (count > 0); // any more data to read?
            }
            catch (Exception)
            {
                Title_update();
                Image_update();
            }
            UpdateTitle();
            UpdateImage();
            Progress1.Visibility = Visibility.Collapsed;
        }
        public static BitmapImage ImageFromRelativePath(FrameworkElement parent, string path)
        {
            var uri = new Uri(parent.BaseUri, path);
            BitmapImage bmp = new BitmapImage();
            bmp.UriSource = uri;
            return bmp;
        }
        #region NavigationHelper registration
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {

            navigationHelper.OnNavigatedTo(e);
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            navigationHelper.OnNavigatedFrom(e);
        }
        #endregion
        private void r1_Click(object sender, RoutedEventArgs e)
        {
            list = no-1;
            pr1.IsActive = true;
            Flipkart();
        }
        private void r2_Click(object sender, RoutedEventArgs e)
        {
            list = no-1; pr2.IsActive = true;
         Infibeam();
        }
        private void r3_Click(object sender, RoutedEventArgs e)
        {
            list = no-1; pr3.IsActive = true;
            Homeshop18();
        }
        private void r4_Click(object sender, RoutedEventArgs e)
        {
            list = no-1; pr4.IsActive = true;
        Amazon();
        }
        private void r5_Click(object sender, RoutedEventArgs e)
        {
            list = no-1; pr5.IsActive = true;
            Crossword();
            
        }
        private void r6_Click(object sender, RoutedEventArgs e)
        {
            list = no-1; pr6.IsActive = true;
            uRead(); 
        }
        private void r7_Click(object sender, RoutedEventArgs e)
        {
            list = no-1; pr7.IsActive = true;
          Landmark();
        }
        private void highlight()
        {
           int x=0;
           if (!p1.Text.Equals("N/A")) { x = Convert.ToInt16(p1.Text); price = x; high = 1; }
           if (!p2.Text.Equals("N/A")) { x = Convert.ToInt16(p2.Text); if (x < price) { price = x; high = 2; } }
           if (!p3.Text.Equals("N/A")){ x = Convert.ToInt16(p3.Text);if(x<price){price=x;high = 3; } }
           if (!p4.Text.Equals("N/A")){ x = Convert.ToInt16(p4.Text);if(x<price){price=x;high = 4; } }
           if (!p5.Text.Equals("N/A")){ x = Convert.ToInt16(p5.Text);if(x<price){price=x;high = 5; } }
           if (!p6.Text.Equals("N/A")){ x = Convert.ToInt16(p6.Text);if(x<price){price=x;high = 6; } }
           if (!p7.Text.Equals("N/A")){ x = Convert.ToInt16(p7.Text);if(x<price){price=x;high = 7; } }
           if (price == -1) return;
           if (high == 1 || p1.Text.Equals(price.ToString()))
           {
              b1.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
              z1.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
           } 
           else
           {
               b1.Background = new SolidColorBrush(Windows.UI.Colors.White);
               z1.Background = new SolidColorBrush(Windows.UI.Colors.White);
           }
           if (high == 2 || p2.Text.Equals(price.ToString()))
           {
               b2.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
               z2.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
           }
           else
           {
               b2.Background = new SolidColorBrush(Windows.UI.Colors.White);
               z2.Background = new SolidColorBrush(Windows.UI.Colors.White);
           } if (high == 3 || p3.Text.Equals(price.ToString()))
           {
               b3.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
               z3.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
           }
           else
           {
               b3.Background = new SolidColorBrush(Windows.UI.Colors.White);
               z3.Background = new SolidColorBrush(Windows.UI.Colors.White);
           } if (high == 4 || p4.Text.Equals(price.ToString()))
           {
               b4.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
               z4.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
           }
           else
           {
               b4.Background = new SolidColorBrush(Windows.UI.Colors.White);
               z4.Background = new SolidColorBrush(Windows.UI.Colors.White);
           } if (high == 5 || p5.Text.Equals(price.ToString()))
           {
               b5.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
               z5.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
           }
           else
           {
               b5.Background = new SolidColorBrush(Windows.UI.Colors.White);
               z5.Background = new SolidColorBrush(Windows.UI.Colors.White);
           } if (high == 6 || p6.Text.Equals(price.ToString()))
           {
               b6.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
               z6.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
           }
           else
           {
               b6.Background = new SolidColorBrush(Windows.UI.Colors.White);
               z6.Background = new SolidColorBrush(Windows.UI.Colors.White);
           } if (high == 7 || p7.Text.Equals(price.ToString()))
           {
               b7.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
               z7.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
           }
           else
           {
               b7.Background = new SolidColorBrush(Windows.UI.Colors.White);
               z7.Background = new SolidColorBrush(Windows.UI.Colors.White);
           }


        }
        private void TextBlock_1PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            
                b1.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
                z1.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
            
        }
        private void TextBlock_1PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (high == 1 || p1.Text.Equals(price.ToString()))
            {
                b1.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
                z1.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
            }
            else
            {
                b1.Background = new SolidColorBrush(Windows.UI.Colors.White);
                z1.Background = new SolidColorBrush(Windows.UI.Colors.White);
            }
        }
        private void TextBlock_2PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            
              b2.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
                z2.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
            

        }
        private void TextBlock_2PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (high == 2 || p2.Text.Equals(price.ToString()))
            {
                b2.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
                z2.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
            }
            else
            {
                b2.Background = new SolidColorBrush(Windows.UI.Colors.White);
                z2.Background = new SolidColorBrush(Windows.UI.Colors.White);
            }
        }
        private void TextBlock_3PointerEntered(object sender, PointerRoutedEventArgs e)
        {
             b3.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
                z3.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
            

        }
        private void TextBlock_3PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (high == 3 || p3.Text.Equals(price.ToString()))
            {
                b3.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
                z3.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
            }
            else
            {
                b3.Background = new SolidColorBrush(Windows.UI.Colors.White);
                z3.Background = new SolidColorBrush(Windows.UI.Colors.White);
            }
        }
        private void TextBlock_4PointerEntered(object sender, PointerRoutedEventArgs e)
        {
           b4.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
                z4.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
            

        }
        private void TextBlock_4PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (high == 4 || p4.Text.Equals(price.ToString()))
            {
                b4.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
                z4.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
            }
            else
            {
                b4.Background = new SolidColorBrush(Windows.UI.Colors.White);
                z4.Background = new SolidColorBrush(Windows.UI.Colors.White);
            }
        }
        private void TextBlock_5PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            b5.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
                z5.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
            

        }
        private void TextBlock_5PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (high == 5 || p5.Text.Equals(price.ToString()))
            {
                b5.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
                z5.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
            }
            else
            {
                b5.Background = new SolidColorBrush(Windows.UI.Colors.White);
                z5.Background = new SolidColorBrush(Windows.UI.Colors.White);
            }
        }
        private void TextBlock_6PointerEntered(object sender, PointerRoutedEventArgs e)
        {
               b6.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
                z6.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
            

        }
        private void TextBlock_6PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (high == 6 || p6.Text.Equals(price.ToString()))
            {
                b6.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
                z6.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
            }
            else
            {
                b6.Background = new SolidColorBrush(Windows.UI.Colors.White);
                z6.Background = new SolidColorBrush(Windows.UI.Colors.White);
            }
        }
        private void TextBlock_7PointerEntered(object sender, PointerRoutedEventArgs e)
        {
              b7.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
                z7.Background = new SolidColorBrush(Windows.UI.Colors.GreenYellow);
            

        }
        private void TextBlock_7PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (high == 7 || p7.Text.Equals(price.ToString()))
            {
                b7.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
                z7.Background = new SolidColorBrush(Windows.UI.Colors.SpringGreen);
            }
            else
            {
                b7.Background = new SolidColorBrush(Windows.UI.Colors.White);
                z7.Background = new SolidColorBrush(Windows.UI.Colors.White);
            }
        }
        private void TextBlock_1Tapped(object sender, TappedRoutedEventArgs e)
        {
            link = 1;
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(BasicPage1));
            }
        }
        private void TextBlock_2Tapped(object sender, TappedRoutedEventArgs e)
        {
            link = 2;
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(BasicPage1));
            }
        }
        private void TextBlock_3Tapped(object sender, TappedRoutedEventArgs e)
        {
            link = 3;
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(BasicPage1));
            }
        }
        private void TextBlock_4Tapped(object sender, TappedRoutedEventArgs e)
        {
            link = 4;
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(BasicPage1));
            }
        }
        private void TextBlock_5Tapped(object sender, TappedRoutedEventArgs e)
        {
            link = 5;
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(BasicPage1));
            }
        }
        private void TextBlock_6Tapped(object sender, TappedRoutedEventArgs e)
        {
            link = 6;
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(BasicPage1));
            }
        }
        private void TextBlock_7Tapped(object sender, TappedRoutedEventArgs e)
        {
            link = 7;
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(BasicPage1));
            }
        }
        private void adDuplexAd_AdLoadingError(object sender, AdDuplex.WinRT.Models.AdLoadingErrorEventArgs e)
        {
            adDuplexAd1.Visibility = Visibility.Collapsed; adDuplexAd2.Visibility = Visibility.Collapsed;
           
        }
        void UpdateISBN()
        {
            TileUpdateManager.CreateTileUpdaterForApplication().Clear();
            TileUpdateManager.CreateTileUpdaterForApplication().EnableNotificationQueue(true);
            ITileSquare310x310Text09 square310x310TileContent = TileContentFactory.CreateTileSquare310x310Text09();
            square310x310TileContent.TextHeadingWrap.Text = "ISBN" + MainPage.s;
            ITileWide310x150Text03 wide310x150TileContent = TileContentFactory.CreateTileWide310x150Text03();
            wide310x150TileContent.TextHeadingWrap.Text = "ISBN:" + MainPage.s;
            ITileSquare150x150Text04 square150x150TileContent = TileContentFactory.CreateTileSquare150x150Text04();
            square150x150TileContent.TextBodyWrap.Text = "ISBN:" + MainPage.s;
            wide310x150TileContent.Square150x150Content = square150x150TileContent;
            square310x310TileContent.Wide310x150Content = wide310x150TileContent;
            TileNotification tileNotification = square310x310TileContent.CreateNotification();
            string tag = "ISBN";
            tileNotification.Tag = tag;
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
        void UpdateImage()
        {
            ITileSquare310x310Image tileContent = TileContentFactory.CreateTileSquare310x310Image();
            tileContent.AddImageQuery = true;
            tileContent.Image.Src = ImageUrl;
            tileContent.Image.Alt = "Web Image";
            ITileWide310x150ImageAndText01 wide310x150Content = TileContentFactory.CreateTileWide310x150ImageAndText01();
            wide310x150Content.TextCaptionWrap.Text = "Last Book:ISBN-" + MainPage.s;
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
        }
        void UpdateTitle()
        {
            ITileSquare310x310Text09 square310x310TileContent = TileContentFactory.CreateTileSquare310x310Text09();
            square310x310TileContent.TextHeadingWrap.Text = "Title:" + Titlen;
            ITileWide310x150Text03 wide310x150TileContent = TileContentFactory.CreateTileWide310x150Text03();
            wide310x150TileContent.TextHeadingWrap.Text = "Title:" + Titlen;
            ITileSquare150x150Text04 square150x150TileContent = TileContentFactory.CreateTileSquare150x150Text04();
            square150x150TileContent.TextBodyWrap.Text = "Title:" + Titlen;
            wide310x150TileContent.Square150x150Content = square150x150TileContent;
            square310x310TileContent.Wide310x150Content = wide310x150TileContent;
            TileNotification tileNotification = square310x310TileContent.CreateNotification();
            string tag = "Title";
            tileNotification.Tag = tag;
            TileUpdateManager.CreateTileUpdaterForApplication().Update(tileNotification);
        }
        private void r1_Click_Image(object sender, RoutedEventArgs e)
        {
            Complete_update();/*
            Image_update();
            Title_update();*/
            Progress1.Visibility = Visibility.Visible;
        }
    }
}
