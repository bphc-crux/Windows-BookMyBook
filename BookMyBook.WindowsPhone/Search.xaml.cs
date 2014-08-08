using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Data.Json;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace BookMyBook
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Search : Page
    {
        List<Items> list = new List<Items>();
        String isb = "";
        public Search()
        {
            this.InitializeComponent();
            
        }

        public async void read()
        {
            if (list.Count <2)
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
                        String d1 = "", d2 = "", d3 = "", d4 = "";
                        for (uint k = 0; k < count; k++)
                        {
                            Items ob = new Items();
                            try
                            {
                                d1 = arr.GetObjectAt(k).GetNamedObject("property1").GetNamedString("text");
                                ob.Title = d1;
                            }
                            catch (Exception e) { }//Message.Show(e.ToString(), "1"+k); }
                            try
                            {
                                d4 = arr.GetObjectAt(k).GetNamedObject("property1").GetNamedString("href");
                                ob.Link = d4;
                            }
                            catch (Exception e) { }//Message.Show(e.ToString(), "1"+k); }
                            try
                            {
                                d3 = arr.GetObjectAt(k).GetNamedObject("property2").GetNamedString("src");
                                ob.Image = new BitmapImage(new Uri(d3, UriKind.Absolute));
                            }
                            catch (Exception e) { }//Message.Show(e.ToString(), "2"+k); }
                            try
                            {
                                d2 = arr.GetObjectAt(k).GetNamedObject("property3").GetNamedString("text");
                                ob.Subtitle = d2;
                            }
                            catch (Exception e) { }//Message.Show(e.ToString(), "3"+k); }
                            list.Add(ob);
                        } itemListView.ItemsSource = list;
                    }
                    else Message.Show("no", "");
                }
                catch (Exception e) { }// Message.Show(e.ToString(), isb.Replace(" ", "+")); }
            }
            else itemListView.ItemsSource = list;
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            isb = e.Parameter.ToString(); Message.Show(isb, ""); read();
        }

       

        private void itemListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            String link = list.ElementAt(itemListView.SelectedIndex).Link;
            link = link.Substring(link.IndexOf("pid") + 4, 13);
            if (this.Frame != null)
            {
                this.Frame.Navigate(typeof(Stores),link);
            }
            //await Launcher.LaunchUriAsync(new Uri(list.ElementAt(itemListView.SelectedIndex).Link)); 
        }

    }
}
