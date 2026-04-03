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

namespace onTime
{
    public partial class DirectionPage : PhoneApplicationPage
    {
        Direction CurrentDirection;
        Direction OppositeDirection;
        public DirectionPage()
        {
            InitializeComponent();
            BuildApplicationBar();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            Line.Text = NavigationContext.QueryString["line_name"];
            CurrentDirection = await Direction.GetDirection(NavigationContext.QueryString["line_id"], false);
            if (CurrentDirection == null)
            {
                NavigationService.GoBack();
                return;
            }

            LastStopTextBlock.Visibility = Visibility.Visible;
            LastStop.Text = CurrentDirection.StopPoints[CurrentDirection.StopPoints.Count - 1].Name;

            StopsListView.ItemsSource = CurrentDirection.StopPoints;

            if (CurrentDirection.HasAnotherDirection)
            {
                OppositeDirection = await Direction.GetDirection(NavigationContext.QueryString["line_id"], true);
            }

            base.OnNavigatedTo(e);
        }

        private void BuildApplicationBar()
        {
            ApplicationBar = new ApplicationBar();

            ApplicationBarIconButton changeDirectionButton = new ApplicationBarIconButton(new Uri("/Assets/AppBar/refresh.png", UriKind.RelativeOrAbsolute));
            changeDirectionButton.Text = AppResources.ChangeDirection;
            changeDirectionButton.Click += ChangeDirectionButton_Click;
            ApplicationBar.Buttons.Add(changeDirectionButton);
        }

        private void ChangeDirectionButton_Click(object sender, EventArgs e)
        {
            if (OppositeDirection != null)
            {
                Direction current = CurrentDirection;
                CurrentDirection = OppositeDirection;
                OppositeDirection = current;

                LastStop.Text = CurrentDirection.StopPoints[CurrentDirection.StopPoints.Count - 1].Name;

                StopsListView.ItemsSource = CurrentDirection.StopPoints;
            }
        }

        private void StopsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StopPoint selectedStopPoint = ((LongListSelector)sender).SelectedItem as StopPoint;

            NavigationService.Navigate(new Uri($"/StopPage.xaml?symbol={selectedStopPoint.Symbol}&name={selectedStopPoint.Name}&line={NavigationContext.QueryString["line_name"]}", UriKind.RelativeOrAbsolute));
        }
    }
}