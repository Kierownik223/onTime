using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using System.Diagnostics;
using Newtonsoft.Json;

namespace onTime
{
    public partial class StopPage : PhoneApplicationPage
    {
        AtomicSchedule schedule;
        public StopPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            StopNameTextBlock.Text = NavigationContext.QueryString["name"].ToLower();

            schedule = await AtomicSchedule.GetAtomicSchedule(NavigationContext.QueryString["symbol"], DateTime.UtcNow);

            DeparturesListView.ItemsSource = schedule.LineSchedules[NavigationContext.QueryString["line"]].Departures;

            base.OnNavigatedTo(e);
        }
    }
}