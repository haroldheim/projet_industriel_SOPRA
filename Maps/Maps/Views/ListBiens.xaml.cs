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

		protected override void OnAppearing()
		{
			base.OnAppearing();
			BienImmo item = new BienImmo();
			item.Titre = "DiZ";
			App.Database.SaveBien(item);
			listBiens.ItemsSource = App.Database.GetBiens();
		}
	}
}
