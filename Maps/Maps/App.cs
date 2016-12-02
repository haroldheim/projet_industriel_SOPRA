using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maps.ViewModel;
using Xamarin.Forms;

namespace Maps
{
    public class App : Application
    {
		private static ViewModelLocator _locator;

		public static ViewModelLocator Locator
		{
			get
			{
				return _locator ?? (_locator = new ViewModelLocator());
			}
		}

		public static BienManager BienManager { get; private set;}

		static BienImmoDatabase database;

		public static BienImmoDatabase Database
		{
			get
			{
				if (database == null)
				{
					database = new BienImmoDatabase();
				}
				return database;
			}
		}

		public App()
		{
			BienManager = new BienManager(new RestService());
			MainPage = new MenuPage();
		}

		protected override void OnStart()
		{
			refreshDatabase();
		}

		public async void refreshDatabase()
		{
			RequestGPSDto req = new RequestGPSDto();
			Filtre filtre = new Filtre();
			req.coordLat = 48.685424;
			req.coordLong = 6.165575;
			filtre.aireRecherche = 10000;
			filtre.prixMax = 100000;
			filtre.prixMin = 0;
			filtre.surfaceMax = 100000;
			filtre.surfaceMin = 0;
			filtre.type = "Vente";
			filtre.typeBien = "Appart";
			req.filtre = filtre;
			List<BienImmoLight> listBiens = new List<BienImmoLight>();
			listBiens = await App.BienManager.GetTaskAsync(req);
			foreach (var item in listBiens)
				App.Database.SaveBien(item);
		}
    }  
}
