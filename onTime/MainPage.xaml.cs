using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using onTime.Resources;
using System.Net.Http;
using Microsoft.Phone.Info;
using System.Reflection;
using System.Net.Http.Headers;

namespace onTime
{
    public partial class MainPage : PhoneApplicationPage
    {
        public HttpClient HttpClient;
        public static MainPage Current;

        public MainPage()
        {
            InitializeComponent();

            HttpClient = new HttpClient();

            var nameHelper = new AssemblyName(Assembly.GetExecutingAssembly().FullName);
            var appVersion = nameHelper.Version.ToString();
            
            HttpClient.DefaultRequestHeaders.Add("User-Agent", $"onTime/{appVersion} ({DeviceStatus.DeviceManufacturer} {DeviceStatus.DeviceName}; Windows Phone {Environment.OSVersion.Version}; https://git.marmak.net.pl/Kierownik223/onTime)");
            HttpClient.DefaultRequestHeaders.CacheControl = new CacheControlHeaderValue
            {
                NoCache = true
            };

            Current = this;

            BuildApplicationBar();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            LinesListView.ItemsSource = await Lines.GetLines();

            base.OnNavigatedTo(e);
        }

        private void LinesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Line selectedLine = ((LongListSelector)sender).SelectedItem as Line;

                NavigationService.Navigate(new Uri($"/DirectionPage.xaml?line_id={selectedLine.Id}&line_name={selectedLine.Name}", UriKind.RelativeOrAbsolute));
            } catch
            {

            }
        }
        
        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();
            
            ApplicationBarMenuItem appBarAboutMenuItem = new ApplicationBarMenuItem(AppResources.AppBarAboutText);
            appBarAboutMenuItem.Click += AppBarAboutMenuItem_Click;
            ApplicationBar.MenuItems.Add(appBarAboutMenuItem);
        }

        private void AppBarAboutMenuItem_Click(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/AboutPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}