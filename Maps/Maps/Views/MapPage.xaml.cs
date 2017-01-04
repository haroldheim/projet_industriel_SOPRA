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

		public MapPage()
        {
			Debug.WriteLine("MapPage()");
            InitializeComponent();
			refreshDatabase();
			NavigationPage.SetHasNavigationBar(this, false);
			MoveMapToCurrentPosition();
			CarouselBiens.ItemSelected += (sender, args) =>
			{
			    var zoo = args.SelectedItem as BienImmoLight;
				current = zoo.Id;
			};
		}

		async void MoveMapToCurrentPosition()
		{
			var locator = CrossGeolocator.Current;
			var position = await locator.GetPositionAsync(10000);
			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1.2)));
		}

		protected async override void OnAppearing()
		{
			Debug.WriteLine("OnAppearing()");

			base.OnAppearing();
			Debug.WriteLine("aireRecherche : " + Settings.aireRecherche);
			var locator = CrossGeolocator.Current;
			var position = await locator.GetPositionAsync(10000);
			RequestGPSDto req = new RequestGPSDto();
			Filtre filtre = new Filtre();
			req.coordLat = position.Latitude;
			req.coordLong = position.Longitude;
			filtre.aireRecherche = Settings.aireRecherche * 1000;
			filtre.prixMax = Settings.prixMax;
			filtre.prixMin = Settings.prixMin;
			filtre.surfaceMax = Settings.surfaceMax;
			filtre.surfaceMin = Settings.surfaceMin;
			filtre.isMaison = Settings.isMaison;
			filtre.isAppartement = Settings.isAppartement;
			req.filtre = filtre;
			IEnumerable<BienImmoLight> listBienMap = App.Database.GetBiensLight(req);
			Debug.WriteLine("taille liste de biens : " + listBienMap.Count()); 
			map.Pins.Clear();
			if (listBienMap.Count() > 0)
			{
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
    }
}
