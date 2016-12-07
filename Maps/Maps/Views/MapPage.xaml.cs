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
			BindingContext = new MapPageViewModel();
			BiensImmoLight = App.Database.GetBiensLight();

			current = BiensImmoLight.First().Id;
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

		protected override void OnAppearing()
		{
			base.OnAppearing();
			IEnumerable<BienImmoLight> listBienMap = App.Database.GetBiensLight();
			foreach (var item in listBienMap)
			{
				Debug.WriteLine(listBienMap.Count());
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

			await Navigation.PushModalAsync(new BienPage(current.ToString()));
		}
    }
}
