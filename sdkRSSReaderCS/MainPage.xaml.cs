using System;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using Microsoft.Phone.Controls;
using System.IO;
using System.ServiceModel.Syndication;
using System.Xml;
using Microsoft.Phone.Tasks;

namespace sdkRSSReaderCS
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

     
        private void webClient_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
              
                    MessageBox.Show(e.Error.Message);
                });
            }
            else
            {
             
                this.State["feed"] = e.Result;

                UpdateFeedList(e.Result);
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
       
            if (this.State.ContainsKey("feed"))
            {
       
                if (feedListBox.Items.Count == 0)
                {
                    UpdateFeedList(State["feed"] as string);
                }
            }
        }

    
        private void UpdateFeedList(string feedXML)
        {
      
            StringReader stringReader = new StringReader(feedXML);
            XmlReader xmlReader = XmlReader.Create(stringReader);
            SyndicationFeed feed = SyndicationFeed.Load(xmlReader);

       
            Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
        
                feedListBox.ItemsSource = feed.Items;

            });

        }

      
        private void feedListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (listBox != null && listBox.SelectedItem != null)
            {
     
                SyndicationItem sItem = (SyndicationItem)listBox.SelectedItem;

       
                if (sItem.Links.Count > 0)
                {
             
                    Uri uri = sItem.Links.FirstOrDefault().Uri;

                    WebBrowserTask webBrowserTask = new WebBrowserTask();
                    webBrowserTask.Uri = uri;
                    webBrowserTask.Show();
                }
            }
        }

        private void ContentPanel_Loaded(object sender, RoutedEventArgs e)
        {
            WebClient webClient = new WebClient();
            webClient.DownloadStringCompleted += new DownloadStringCompletedEventHandler(webClient_DownloadStringCompleted);
            webClient.DownloadStringAsync(new System.Uri("http://www.sevdanurgenc.com/feed"));

        }
    }
}
