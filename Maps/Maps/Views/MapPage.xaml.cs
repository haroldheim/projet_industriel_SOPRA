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
			MoveMapToCurrentPosition();
			SetPinsOnMap();
		}

		async void OnSearchPageButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SearchPage());

		}


		async void MoveMapToCurrentPosition()
		{
			var locator = CrossGeolocator.Current;
			var position = await locator.GetPositionAsync(10000);
			MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1.2)));
		}

		async void SetPinsOnMap()
		{
			List<BienImmo> Biens = await App.BienManager.GetTaskAsync();
			foreach (var bien in Biens)
			{
				var pin = new Pin()
				{
					Position = new Position(bien.CoordLong, bien.CoordLat),
					Label = bien.Titre
				};
				MyMap.Pins.Add(pin);
			}
		}
    }
}
