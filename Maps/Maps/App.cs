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
			req.filtre = filtre;
			List<BienImmoLight> listBiens = new List<BienImmoLight>();
			listBiens = await App.BienManager.GetTaskAsync(req);

			Debug.WriteLine("taille : " + listBiens.Count());

			foreach (var item in listBiens)
			{
				Debug.WriteLine("item : " + item.Titre);

				App.Database.SaveBien(item);

			}
			App.Database.displayTable();
		}
    }  
}
