﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Plugin.Geolocator;

namespace Maps
{
    public partial class MapPage : ContentPage
    {
		Map map;
		SearchBar searchBar;
		Button filtreButton;

        public MapPage()
        {
            InitializeComponent();

			//créé la map
			map = new Map
			{
				IsShowingUser = true,
				HeightRequest = 100,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			//créé la barre de recherche et filtre
			searchBar = new SearchBar
			{
				Placeholder = "Adresse",
				WidthRequest = 250
			};
			filtreButton = new Button { Text = "Filtrer" , Margin = new Thickness(10,0,0,0)};

			var segments = new StackLayout
			{
				Orientation = StackOrientation.Horizontal,
				Children = { searchBar, filtreButton }
			};

			var stack = new StackLayout { Spacing = 0, Margin = new Thickness(0, 20, 0, 0),};
			stack.Children.Add(segments);
			stack.Children.Add(map);
			Content = stack;

			MoveMapToCurrentPosition();
		}

		async void MoveMapToCurrentPosition()
		{
			var locator = CrossGeolocator.Current;
			var position = await locator.GetPositionAsync(10000);
			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1.2)));
		}

		protected async override void OnAppearing()
		{
			base.OnAppearing();
			IEnumerable<BienImmoLight> listBienMap = App.Database.GetBiensLight();
			foreach (var item in listBienMap)
			{
				var position = new Position(item.coordLat, item.coordLong);
				var pin = new Pin
				{
					Position = position,
					Label = item.Titre
				};
				map.Pins.Add(pin);
			}
		}
    }
}
