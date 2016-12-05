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
        public MapPage()
        {
            InitializeComponent();
			BindingContext = new MapPageViewModel();
			MoveMapToCurrentPosition();
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
    }
}
