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

namespace AppTrips.UWP
{
    public sealed partial class MainPage
    {
        public MainPage()
        {
            
            this.InitializeComponent();
            Xamarin.FormsMaps.Init("yrfNZnT1Rvoy6dHZIh1P~LrDnXIKH87px6azOdkXjCA~AvHmz2a6jZItnWwhigvUJRq-YfyZmNTvufWrt49VbW4GCWOCUn69nwLiioGeu0cO");

            LoadApplication(new AppTrips.App());
        }
    }
}
