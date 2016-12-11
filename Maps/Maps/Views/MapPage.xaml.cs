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

			RequestGPSDto req = new RequestGPSDto();
			Filtre filtre = new Filtre();
			req.coordLat = 48.685424;
			req.coordLong = 6.165575;
			filtre.aireRecherche = 3000;
			req.filtre = filtre;
			CarouselBiens.ItemsSource =  App.Database.GetBiensLight();
			IEnumerable<BienImmoLight> listBienMap = App.Database.GetBiensLight();

			map.Pins.Clear();
			foreach (var item in listBienMap)
			{
				var position = new Position(item.coordLat, item.coordLong);
				var pin = new Pin
				{
					Position = position,
					Label = item.Titre
				};

				map.Pins.Add(pin);
				pin.Clicked += (sender, e) => OnPinClicked(item.Id);

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
			var bienPage = new BienPage();
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
			var bienPage = new BienPage();
			bienPage.BindingContext = bienBdd;
			await Navigation.PushAsync(bienPage);

		}
    }
}
