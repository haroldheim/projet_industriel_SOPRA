using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Maps
{
	public partial class ListeBiens : ContentPage
	{
		BienImmoManager manager;

		public ListeBiens()
		{
			InitializeComponent();

			manager = BienImmoManager.DefaultManager;

		}

		protected override async void OnAppearing()
		{
			base.OnAppearing();

			await RefreshItems(true, syncItems: true);
		}

		private  async Task RefreshItems(bool v, bool syncItems)
		{
			listeBiens.ItemsSource = await manager.GetBienImmoAsync(syncItems);
		}
}
}
