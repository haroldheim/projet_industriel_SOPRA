using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Maps.Helpers;
using MvvmHelpers;
using Plugin.Connectivity;
using Plugin.Geolocator;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Maps
{
	public class MapPageViewModel : ViewModelBase
	{
		public IEnumerable<BienImmoLight> BiensLight { get; set; }

		public MapPageViewModel(Page page) : base(page)
		{
			BiensLight = new List<BienImmoLight>();
			Title = "Search";
		}

		public async Task ExecuteGetBiensCommand(Position position)
		{
			if (IsBusy)
				return;
			
			IsBusy = true;
			var showAlert = false;

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
			filtre.isSale = Settings.isSale;
			filtre.isRental = Settings.isRental;

			req.filtre = filtre;
			try
			{
				if (!CrossConnectivity.Current.IsConnected)
					await page.DisplayAlert("No signal detected", "Only previously loaded properties will be shown.", "OK");
				else {
					showAlert = await App.BienManager.CheckWs(req);

					if (showAlert)
						await page.DisplayAlert("Oops, our server seems to be down", "Only previously loaded properties will be shown.", "OK");
					else {
						var biens = await App.BienManager.GetTaskAsync(req);

						foreach (var item in biens)
						{
							App.Database.SaveBien(item);
							Debug.WriteLine(item.Id);
							BienImmo bienRest = new BienImmo();
							bienRest = await App.BienManager.GetBienAsync(item.Id);
							App.Database.SaveBienDetailed(bienRest);
						}
					}
				}

			}
			catch(Exception e)
			{
				Debug.WriteLine(@"ERROR {0}", e.Message);
			}
			finally
			{
				IsBusy = false;
			}


		}

		public async Task ExecuteGetBiensSQLite(Position position)
		{
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
			filtre.isSale = Settings.isSale;
			filtre.isRental = Settings.isRental;

			req.filtre = filtre;

			BiensLight = App.Database.GetBiensLight(req);
		}


	}
}
