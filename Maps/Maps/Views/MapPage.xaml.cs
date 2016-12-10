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

namespace Maps
{
    public partial class MapPage : ContentPage
    {
		int current;
		public IEnumerable<BienImmoLight> BiensImmoLight { get; set; }

		public MapPage()
        {
            InitializeComponent();
			//BiensImmoLight = App.Database.GetBiensLight();
			//Debug.WriteLine("taille de la liste :D " + BiensImmoLight.Count());
			//BindingContext = new MapPageViewModel();
			NavigationPage.SetHasNavigationBar(this, false);
			//current = BiensImmoLight.First().Id;
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
			base.OnAppearing();
			RequestGPSDto req = new RequestGPSDto();
			Filtre filtre = new Filtre();
			req.coordLat = 48.685424;
			req.coordLong = 6.165575;
			filtre.aireRecherche = 10000;
			req.filtre = filtre;
			CarouselBiens.ItemsSource =  App.Database.GetBiensLight();
			IEnumerable<BienImmoLight> listBienMap = App.Database.GetBiensLight();
			foreach (var item in listBienMap)
			{
				var position = new Position(item.coordLat, item.coordLong);
				var pin = new Pin
				{
					Position = position,
					Label = item.Titre
				};
				map.Pins.Add(pin);
			}
		}

		async void  OnFiltreClicked(object sender, EventArgs args)
		{
			
			var filtrePage = new SearchPage();
			await Navigation.PushModalAsync(filtrePage);
		}



		async void OnTapGestureRecognizerTapped(object sender, EventArgs args) {
			
			BienImmo bienRest = new BienImmo();
			bienRest = await App.BienManager.GetBienAsync(current);
			App.Database.SaveBienDetailed(bienRest);

			BienImmo bienBdd = new BienImmo();
			Debug.WriteLine("current :D " + current);
			bienBdd = App.Database.GetSingleBien(current);

			//Debug.WriteLine("bien trouvé : " + bienBdd.Titre);
			var bienPage = new BienPage();
			bienPage.BindingContext = bienBdd;
			await Navigation.PushAsync(bienPage);

		}
    }
}
