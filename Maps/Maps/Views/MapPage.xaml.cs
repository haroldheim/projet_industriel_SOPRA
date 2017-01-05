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

namespace Maps
{
    public partial class MapPage : ContentPage
    {
		int current;
		public IEnumerable<BienImmoLight> BiensImmoLight { get; set; }
		Geocoder geoCoder;
		Position pos;

		public MapPage()
        {
			Debug.WriteLine("MapPage()");
            InitializeComponent();
			refreshDatabase();
			NavigationPage.SetHasNavigationBar(this, false);

			geoCoder = new Geocoder();

			MoveMapToCurrentPosition();
			CarouselBiens.ItemSelected += (sender, args) =>
			{
			    var zoo = args.SelectedItem as BienImmoLight;
				current = zoo.Id;
			};
			Debug.WriteLine("fin const");
		}

		async void MoveMapToPosition(Position position)
		{
			map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromMiles(1.2)));
			RequestGPSDto req = new RequestGPSDto();
			Filtre filtre = new Filtre();
			req.coordLat = pos.Latitude;
			req.coordLong = pos.Longitude;
			filtre.aireRecherche = Settings.aireRecherche * 1000;
			filtre.prixMax = Settings.prixMax;
			filtre.prixMin = Settings.prixMin;
			filtre.surfaceMax = Settings.surfaceMax;
			filtre.surfaceMin = Settings.surfaceMin;
			filtre.isMaison = Settings.isMaison;
			filtre.isAppartement = Settings.isAppartement;
			filtre.isSale = Settings.isSale;
			filtre.isRental = Settings.isRental;

			req.filtre = filtre;
			IEnumerable<BienImmoLight> listBienMap = App.Database.GetBiensLight(req);
			Debug.WriteLine("taille liste de biens : " + listBienMap.Count());
			map.Pins.Clear();
			if (listBienMap.Count() > 0)
			{
				CarouselBiens.IsVisible = true;
				CarouselBiens.ItemsSource = listBienMap;

				foreach (var item in listBienMap)
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
			else {
				CarouselBiens.IsVisible = false;
			}
		}

		async void MoveMapToCurrentPosition()
		{
			var locator = CrossGeolocator.Current;
			var position = await locator.GetPositionAsync(10000);
			pos = new Position(position.Latitude, position.Longitude);
			map.MoveToRegion(MapSpan.FromCenterAndRadius(pos, Distance.FromMiles(1.2)));
		}

		protected async override void OnAppearing()
		{
			Debug.WriteLine("OnAppearing()");
			base.OnAppearing();
			var locator = CrossGeolocator.Current;
			var position = await locator.GetPositionAsync(10000);
			RequestGPSDto req = new RequestGPSDto();
			Filtre filtre = new Filtre();
			req.coordLat = pos.Latitude;
			req.coordLong = pos.Longitude;
			filtre.aireRecherche = Settings.aireRecherche * 1000;
			filtre.prixMax = Settings.prixMax;
			filtre.prixMin = Settings.prixMin;
			filtre.surfaceMax = Settings.surfaceMax;
			filtre.surfaceMin = Settings.surfaceMin;
			filtre.isMaison = Settings.isMaison;
			filtre.isAppartement = Settings.isAppartement;
			filtre.isSale = Settings.isSale;
			filtre.isRental = Settings.isRental;

			req.filtre = filtre;
			IEnumerable<BienImmoLight> listBienMap = App.Database.GetBiensLight(req);
			Debug.WriteLine("taille liste de biens : " + listBienMap.Count()); 
			map.Pins.Clear();
			if (listBienMap.Count() > 0)
			{
				CarouselBiens.IsVisible = true;
				CarouselBiens.ItemsSource = listBienMap;

				foreach (var item in listBienMap)
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
			else {
				CarouselBiens.IsVisible = false;
			}
		}

		async void  OnFiltreClicked(object sender, EventArgs args)
		{
			
			var filtrePage = new SearchPage();
			await Navigation.PushModalAsync(filtrePage);
		}

		async void OnPinClicked(int id)
		{
			current = id;
			BienImmo bienRest = new BienImmo();
			bienRest = await App.BienManager.GetBienAsync(current);
			App.Database.SaveBienDetailed(bienRest);

			BienImmo bienBdd = new BienImmo();
			Debug.WriteLine("current :D " + current);
			bienBdd = App.Database.GetSingleBien(current);

			Debug.WriteLine("bien trouvé : " + bienBdd.Titre);
			var bienPage = new BienPage(bienBdd);
			bienPage.BindingContext = bienBdd;
			await Navigation.PushAsync(bienPage);
		}


		async void OnTapGestureRecognizerTapped(object sender, EventArgs args) {
			
			BienImmo bienRest = new BienImmo();
			bienRest = await App.BienManager.GetBienAsync(current);
			App.Database.SaveBienDetailed(bienRest);

			BienImmo bienBdd = new BienImmo();
			Debug.WriteLine("current :D " + current);
			bienBdd = App.Database.GetSingleBien(current);

			App.Database.displayTable();
			Debug.WriteLine("bien trouvé : " + bienBdd.Titre);
			var bienPage = new BienPage(bienBdd);
			//bienPage.BindingContext = bienBdd;
			await Navigation.PushAsync(bienPage);

		}

		public async void refreshDatabase()
		{
			var locator = CrossGeolocator.Current;
			var position = await locator.GetPositionAsync(10000);
			RequestGPSDto req = new RequestGPSDto();
			Filtre filtre = new Filtre();
			req.coordLat = position.Latitude;
			req.coordLong = position.Longitude;
			filtre.aireRecherche = Settings.aireRecherche * 1000;
			req.filtre = filtre;
			List<BienImmoLight> listBiens = new List<BienImmoLight>();
			listBiens = await App.BienManager.GetTaskAsync(req);

			Debug.WriteLine("taille : " + listBiens.Count());

			foreach (var item in listBiens)
			{
				Debug.WriteLine("item : " + item.sousTitre);

				App.Database.SaveBien(item);

			}
			App.Database.displayTable();
		}

		async void OnGeocodeButtonClicked(object sender, EventArgs e)
		{
			if (!string.IsNullOrWhiteSpace(Address.Text))
			{
				var address = Address.Text;
				var approximateLocations = await geoCoder.GetPositionsForAddressAsync(address);
				foreach (var position in approximateLocations)
				{
					pos = position;
					MoveMapToPosition(position);
				}
			}
		}
    }
}
