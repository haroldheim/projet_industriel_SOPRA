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

		protected  override void OnAppearing()
		{
			base.OnAppearing();
			listBiens.ItemsSource =  App.Database.GetBiensLight();
		}
	}
}
