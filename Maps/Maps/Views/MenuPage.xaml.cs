﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;


namespace Maps
{
	public partial class MenuPage : TabbedPage
    {
		public MenuPage()
		{
			InitializeComponent();
		}

		protected override void OnAppearing()
		{
			base.OnAppearing();

			App.Database.GetBiens();
		}
    }
}
