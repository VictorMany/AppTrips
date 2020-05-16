using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.Maps;
using AppTrips.Models;
using AppTrips.Services;

namespace AppTrips.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TripMapPage : ContentPage
    {
        public TripMapPage(TripModel tripSelected)  //posicionamiento del mapa
        {
            InitializeComponent();

            MapTrip.MoveToRegion(
               MapSpan.FromCenterAndRadius(
                   new Position(tripSelected.Latitude, tripSelected.Longitude),
                   Distance.FromMiles(.5)
           ));

            tripSelected.ImageUrl = new ImageService().SaveImageFromBase64(tripSelected.ImageUrl);
            MapTrip.Trip = tripSelected;

            MapTrip.Pins.Add(  //PIN
                new Pin
                {
                    Type = PinType.Place,
                    Label = tripSelected.Title,
                    Position = new Position(tripSelected.Latitude, tripSelected.Longitude)
                }
            );

            Title.Text = tripSelected.Title;           //DATOS DEL BOX VIEW
            Date.Text = tripSelected.TripDate.ToShortDateString();
            Rating.Text = $"{tripSelected.Rating} Estrellas";
            Notes.Text = tripSelected.Notes;

        }
    }
}