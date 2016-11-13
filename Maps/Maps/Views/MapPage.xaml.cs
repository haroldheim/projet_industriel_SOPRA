using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Maps
{
    public partial class MapPage : ContentPage
    {
		BienImmoManager manager;

        public MapPage()
        {
            InitializeComponent();


            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(48.669075, 6.155275), Distance.FromMeters(500)));


        }

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			// Set syncItems to true in order to synchronize the data on startup when running in offline mode
			await SetPins();
		}

		private async Task SetPins()
		{
			manager = BienImmoManager.DefaultManager;
			List<BienImmo> listeBiens = await manager.GetBienImmo();

			foreach (var biens in listeBiens)
			{
				var pin = new Pin()
				{
					Position = new Position(biens.CoordLat, biens.CoordLong),
					Label = biens.Titre
				};

				MyMap.Pins.Add(pin);
			}
		}
    }
}
