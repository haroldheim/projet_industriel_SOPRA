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

		public App()
		{
			MainPage = new MenuPage();
		}
    }   
}
