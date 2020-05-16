using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AppTrips.Droid.Renders;
using AppTrips.Renders;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(myEntry), typeof(MyEntryRender))]
namespace AppTrips.Droid.Renders
{
    public class MyEntryRender : EntryRenderer
    {
       public MyEntryRender(Context context) : base(context) 
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(Android.Graphics.Color.LightGreen);
            }
        }
    }
}