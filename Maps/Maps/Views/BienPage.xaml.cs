using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Maps
{
	public partial class BienPage : ContentPage
	{
		public BienPage(string titre)
		{
			InitializeComponent();
			bien.Text = titre;
		}

		async void OnRetourClicked(object sender, EventArgs args)
		{

			await Navigation.PopModalAsync();
		}
	}
}
