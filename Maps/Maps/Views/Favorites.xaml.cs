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

		async void OnListItemSelected(object sender, SelectedItemChangedEventArgs e)
		{
			await Navigation.PushAsync(new BienPage(e.SelectedItem as BienImmo));
		}
	}
}
