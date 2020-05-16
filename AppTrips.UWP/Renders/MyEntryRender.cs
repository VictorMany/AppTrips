using AppTrips.Renders;
using AppTrips.UWP.Renders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Xaml.Media;
using Xamarin.Forms;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(myEntry), typeof(MyEntryRender))]
namespace AppTrips.UWP.Renders
{
    public class MyEntryRender : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.Background = new SolidColorBrush(Colors.Purple);
                Control.BackgroundFocusBrush = new SolidColorBrush(Colors.MediumPurple);
            }
        }
    }
}
