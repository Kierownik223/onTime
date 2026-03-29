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
        List<Departure> departures;
        public StopPage()
        {
            InitializeComponent();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            StopNameTextBlock.Text = NavigationContext.QueryString["name"].ToLower();

            schedule = await AtomicSchedule.GetAtomicSchedule(NavigationContext.QueryString["symbol"], DateTime.UtcNow);

            departures = schedule.LineSchedules[NavigationContext.QueryString["line"]].Departures;

            var grouped = departures
                .GroupBy(d => TimeSpan.FromSeconds(d.ScheduledDepartureSec).Hours)
                .Select(g => new DepartureGroup(
                    g.Key.ToString("D2"),
                    g.OrderBy(x => x.ScheduledDepartureSec)))
                .ToList();

            DeparturesListView.ItemsSource = grouped;

            base.OnNavigatedTo(e);
        }
    }
}