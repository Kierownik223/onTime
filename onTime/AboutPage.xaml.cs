using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using Microsoft.Phone.Tasks;
using System.Reflection;

namespace onTime
{
    public partial class AboutPage : PhoneApplicationPage
    {
        public AboutPage()
        {
            InitializeComponent();
            var nameHelper = new AssemblyName(Assembly.GetExecutingAssembly().FullName);

            var version = nameHelper.Version;

            AppVersion.Text = version.Major + "." + version.Minor + "." + version.Build + "." + version.Revision;
        }

        private void ApplicationIcon_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            EmailComposeTask emailTask = new EmailComposeTask();
            emailTask.To = "mersey@emma.casino";
            emailTask.Show();
        }
    }
}