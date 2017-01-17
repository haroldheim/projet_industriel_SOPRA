using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maps.Helpers;
using Xamarin.Forms;

namespace Maps
{
    public class App : Application
    {
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
		}


    }  
}
