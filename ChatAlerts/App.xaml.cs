using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ChatAlerts
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static bool IsDesingnMode { get; private set; } = true;
        protected override async void OnStartup(StartupEventArgs e)
        {
            IsDesingnMode = false;
        }
    }
}
