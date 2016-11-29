using System;
using System.Collections.Generic;
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
    }  
}
