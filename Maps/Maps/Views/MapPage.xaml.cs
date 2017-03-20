using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;
using Maps.Helpers;
using Plugin.Permissions;
using Plugin.Permissions.Abstractions;

namespace Maps
{
    public partial class MapPage : ContentPage
    {
		int current;
		public IEnumerable<BienImmoLight> BiensImmoLight { get; set; }
		Geocoder geoCoder;
		Position pos;
		bool start = true;
		bool startSearch = true;
		bool newSearch = false;

        public int clickedfiltre = 0;

		MapPageViewModel viewModel;

		public MapPage()
        {
            InitializeComponent();
			BindingContext = viewModel = new MapPageViewModel(this);

			geoCoder = new Geocoder();

			NavigationPage.SetHasNavigationBar(this, false);

			CarouselBiens.ItemSelected += (sender, args) =>
			{
			    var zoo = args.SelectedItem as BienImmoLight;
				current = zoo.Id;
			};
		}

		void MoveMapToPosition(Position position)
		{
			map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1.2)));
		}

		private async void MoveMapToCurrentPosition()
		{
			var locator = CrossGeolocator.Current;
			var position = await locator.GetPositionAsync(10000);
			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1.2)));
		}

		private void MoveMapToFrance()
		{
			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(46.880169, 2.659286), Distance.FromMiles(300)));
		}

		private void SetPinsOnMap()
		{
			map.Pins.Clear();
			foreach (var item in viewModel.BiensLight)
			{
				var positionPin = new Position(item.coordLat, item.coordLong);
				var pin = new Pin
				{
					Position = positionPin,
					Label = item.Titre,
					Address = item.sousTitre
				};

					map.Pins.Add(pin);
					pin.Clicked += (sender, e) => OnPinClicked(item.Id);
			}
		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			Settings.isDoubleClicked = true;

			if (start)
			{
				start = false;

				try
				{
					var status = await CrossPermissions.Current.CheckPermissionStatusAsync(Permission.Location);
					if (status != PermissionStatus.Granted)
					{
						if (await CrossPermissions.Current.ShouldShowRequestPermissionRationaleAsync(Permission.Location))
						{
							await DisplayAlert("Need location", "Gunna need that location", "OK");
						}

						var results = await CrossPermissions.Current.RequestPermissionsAsync(new[] { Permission.Location });
						status = results[Permission.Location];
					}

					if (status == PermissionStatus.Granted)
					{
						var locator = CrossGeolocator.Current;
						var position = await locator.GetPositionAsync(10000);
						pos = new Position(position.Latitude, position.Longitude);

						MoveMapToCurrentPosition();
						
					}
					else if (status != PermissionStatus.Unknown)
					{
						MoveMapToFrance();
						await DisplayAlert("Location Denied", "Please enable geolocation to gather properties around you !", "OK");
					}
				}
				catch (Exception ex)
				{

				}
			}

			if (startSearch || newSearch || Settings.isModified)
			{
				await viewModel.ExecuteGetBiensCommand(pos);
				Settings.isModified = false;
				newSearch = false;
				startSearch = false;
			}

			await viewModel.ExecuteGetBiensSQLite(pos);

			if (viewModel.BiensLight.Count() == 0)
				CarouselBiens.IsVisible = false;
			else {
				CarouselBiens.IsVisible = true;
				CarouselBiens.ItemsSource = viewModel.BiensLight;
			}
				
			SetPinsOnMap();
		}


		async void  OnFiltreClicked(object sender, EventArgs args)
		{
			if (Settings.isDoubleClicked)
			{
                Settings.isDoubleClicked = false;
                var filtrePage = new SearchPage();
				await Navigation.PushModalAsync(filtrePage);
			}
		}

		async void OnPinClicked(int id)
		{
			BienImmo bienBdd = new BienImmo();
			bienBdd = App.Database.GetSingleBien(id);

			var bienPage = new BienPage(bienBdd);
			await Navigation.PushAsync(bienPage);
		}


		async void OnTapGestureRecognizerTapped(object sender, EventArgs args) {

			BienImmo bienBdd = new BienImmo();
			bienBdd = App.Database.GetSingleBien(current);

			var bienPage = new BienPage(bienBdd);
			await Navigation.PushAsync(bienPage);

		}

		async void OnGeocodeButtonClicked(object sender, EventArgs e)
		{
			newSearch = true;
			var address = Address.Text;
			var approximateLocations = await geoCoder.GetPositionsForAddressAsync(address);
			foreach (var position in approximateLocations)
			{
				pos = position;
				MoveMapToPosition(position);
				OnAppearing();
			}
		}

		async void OnGeoLocationButtonClicked(object sender, EventArgs e)
		{
			var locator = CrossGeolocator.Current;
			if (locator.IsGeolocationEnabled)
			{
				var position = await locator.GetPositionAsync(10000);
				pos = new Position(position.Latitude, position.Longitude);
				MoveMapToPosition(pos);
				OnAppearing();
			}
			else {
				await DisplayAlert("Geolocation is disabled", "Please enable geolocation to center on your position !", "OK");
			}
		}

    }
}
