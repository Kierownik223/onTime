using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;

namespace onTime
{
    public partial class DirectionPage : PhoneApplicationPage
    {
        Direction CurrentDirection;
        Direction OppositeDirection;
        public DirectionPage()
        {
            InitializeComponent();
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
    }
}