using AppTrips.Models;
using AppTrips.Services;
using AppTrips.Views;
using Plugin.Media;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace AppTrips.ViewModels
{
    public class TripDetailViewModel : BaseViewModel
    {
        private int id;

        Command _saveCommand;
        public Command SaveCommand => _saveCommand ?? (_saveCommand = new Command(SaveAction));

        Command _eliminateCommand;
        public Command EliminateCommand => _eliminateCommand ?? (_eliminateCommand = new Command(EliminateAction));

        Command _GetLocationCommand;
        public Command GetLocationCommand => _GetLocationCommand ?? (_GetLocationCommand = new Command(GetLocationAction));

        Command _TakePictureCommand;
        public Command TakePictureCommand => _TakePictureCommand ?? (_TakePictureCommand = new Command(TakePictureAction));

        Command _SelectPictureCommand;
        public Command SelectPictureCommand => _SelectPictureCommand ?? (_SelectPictureCommand = new Command(SelectPictureAction));

        Command _MapCommand;
        public Command MapCommand => _MapCommand ?? (_MapCommand = new Command(MapAction));

        string _Title;
        public string Title 
        { 
            get => _Title;
            set => SetProperty(ref _Title, value); 
        }

        string _Notes;
        public string Notes
        {
            get => _Notes;
            set => SetProperty(ref _Notes, value);
        }

        DateTime _TripDate;
        public DateTime TripDate
        {
            get => _TripDate;
            set => SetProperty(ref _TripDate, value);
        }

        double _Latitude;
        public double Latitude
        {
            get => _Latitude;
            set => SetProperty(ref _Latitude, value);
        }

        double _Longitude;
        public double Longitude
        {
            get => _Longitude;
            set => SetProperty(ref _Longitude, value);
        }

        int _Rating;
        public int Rating
        {
            get => _Rating;
            set => SetProperty(ref _Rating, value);
        }

        string _ImageUrl;
        public string ImageUrl
        {
            get => _ImageUrl;
            set => SetProperty(ref _ImageUrl, value);
        }

        public TripDetailViewModel()
        {
        }

        public TripDetailViewModel(TripModel trip)
        {
            if (trip != null)
            {
                id = trip.ID;
                Title = trip.Title;
                Notes = trip.Notes;
                Latitude = trip.Latitude;
                Longitude = trip.Longitude;
                Rating = trip.Rating;
                TripDate = trip.TripDate;
                ImageUrl = trip.ImageUrl;
            }
        }

        private async void SaveAction()
        {
            IsBusy = true;
            if (id == 0)
            {
    
                ApiResponse response = await new ApiService().PostDataAsync("trips", new TripModel
                {
                    Title = this.Title,
                    Notes = this.Notes,
                    Latitude = this.Latitude,
                    Longitude = Longitude,
                    Rating = Rating,
                    TripDate = TripDate,
                    ImageUrl = ImageUrl
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("AppTrips", "Error al crear el viaje", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("AppTrips", response.Message, "Ok");
                    return;
                }
                TripsViewModel.GetInstance().RefreshTrips();
                await Application.Current.MainPage.DisplayAlert("AppTrips", response.Message, "Ok");
            }
            else
            {
         
                ApiResponse response = await new ApiService().PutDataAsync("trips", new TripModel
                {
                    ID = id,
                    Title = this.Title,
                    Notes = this.Notes,
                    Latitude = this.Latitude,
                    Longitude = Longitude,
                    Rating = Rating,
                    TripDate = TripDate,
                    ImageUrl = ImageUrl
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("AppTrips", "Error al actualizar el viaje", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("AppTrips", response.Message, "Ok");
                    return;
                }
                TripsViewModel.GetInstance().RefreshTrips();
                await Application.Current.MainPage.DisplayAlert("AppTrips", response.Message, "Ok");
            }

            // Emulamos que esta haciendo algo
            //await Task.Delay(5000);

            IsBusy = false;
            //await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void EliminateAction()
        {
          
                ApiResponse response = await new ApiService().DeleteDataAsync("trips", new TripModel
                {
                   ID = this.id
                });
                if (response == null)
                {
                    await Application.Current.MainPage.DisplayAlert("AppTrips", "Error al eliminar el viaje", "Ok");
                    return;
                }
                if (!response.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("AppTrips", response.Message, "Ok");
                    return;
                }
                TripsViewModel.GetInstance().RefreshTrips();
                await Application.Current.MainPage.DisplayAlert("AppTrips", response.Message, "Ok");
            
          
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private void MapAction()   //Obtenemos los datos para agregarlos al cuadro del mapa
        {
            Application.Current.MainPage.Navigation.PushAsync(new TripMapPage(new TripModel
            {
                Title = Title,
                Notes = Notes,
                Latitude = Latitude,
                Longitude = Longitude,
                Rating = Rating,
                TripDate = TripDate,
                ImageUrl = ImageUrl
            }));
        }

        private async void GetLocationAction()
        {
            try
            {
                var location = await Geolocation.GetLastKnownLocationAsync();

                if (location != null)
                {
                    //Console.WriteLine($"Latitude: {location.Latitude}, Longitude: {location.Longitude}, Altitude: {location.Altitude}");
                    Latitude = location.Latitude;
                    Longitude = location.Longitude;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }
        }

        private async void TakePictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("No Camera", ":( No camera available.", "OK");
                return;
            }

            var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
            {
                Directory = "Sample",
                Name = "test.jpg"
            });

            if (file == null)
                return;

            ImageUrl = await new ImageService().ConvertImageFileToBase64(file.Path);
            await Application.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");
        }

        private async void SelectPictureAction()
        {
            if (Device.RuntimePlatform == Device.UWP)
            {
                await CrossMedia.Current.Initialize();
            }

            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Seleccionar fotografías no soportada", "OK");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium
            });

            if (file == null)
                return;

             ImageUrl = await new ImageService().ConvertImageFileToBase64(file.Path);
        }
    }
}
