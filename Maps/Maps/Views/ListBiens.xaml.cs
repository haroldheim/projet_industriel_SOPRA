using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace Maps
{
	public partial class ListBiens : ContentPage
	{
		public ListBiens()
		{
			InitializeComponent();
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();


			listBiens.ItemsSource = await App.BienManager.GetTaskAsync();
		}
	}
}
