using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Maps
{
	public partial class Favorites : ContentPage
	{
		public Favorites()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();
			listView.ItemsSource =  App.Database.GetBiensFavorites();
		} 
	}
}
