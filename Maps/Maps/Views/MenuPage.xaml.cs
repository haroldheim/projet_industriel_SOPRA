using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;


namespace Maps
{
    public partial class MenuPage : ContentPage
    {
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
