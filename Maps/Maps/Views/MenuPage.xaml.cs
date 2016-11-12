using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Microsoft.WindowsAzure.MobileServices;


namespace Maps
{
    public partial class MenuPage : ContentPage
    {
        public static MobileServiceClient MobileService = new MobileServiceClient("https://findurhome.azurewebsites.net");
		BienImmo bien = new BienImmo { Titre = "Awesome item" };

        public MenuPage()
        {
            InitializeComponent();
            
         }

		async void OnMapButtonClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new MapPage());
		}

		async void OnListeClicked(object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ListeBiens());
		}
    }
}
