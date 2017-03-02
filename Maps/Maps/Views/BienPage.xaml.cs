using System;
using System.Collections.Generic;
using System.Diagnostics;
using Plugin.Connectivity;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Maps
{
	public partial class BienPage : ContentPage
	{
		FavoriteCheck favorites;
		BienImmo bien;
		Button button;
		Map map;

		public BienPage(BienImmo bien)
		{
			InitializeComponent();
			this.bien = bien;
			favorites = new FavoriteCheck
			{
				Checked = bien.isFavorite,
				HorizontalOptions=LayoutOptions.EndAndExpand,
			};

			favorites.Clicked += OnFavClicked;

			button = new Button
			{
				Text = "3D Model"
			};
			button.Clicked += On3DModelClicked;

			map = new Map
			{
				HeightRequest = 300,
				WidthRequest = 960,
				VerticalOptions = LayoutOptions.FillAndExpand
			};

			map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(bien.CoordLat, bien.CoordLong), Distance.FromMiles(1.2)));

			var pin = new Pin
			{
				Type = PinType.Place,
				Position = new Position(bien.CoordLat,bien.CoordLong),
				Label = bien.Titre,
				Address = bien.Soustitre
			};
			map.Pins.Add(pin);

 			StackLayout stackLayout = new StackLayout
			{
				Spacing = 0,
				Padding = 10,
				Children = {
					new StackLayout {
						Orientation=StackOrientation.Horizontal,
						Children={
							new Label
							{
								Text=bien.Titre,
								FontAttributes=FontAttributes.Bold
							},
							favorites
						}
					},

					new Label {
						Text=bien.Soustitre,
						FontSize=11,
						Margin = new Thickness(0,0,0,15)
					},
					new Image {
						Source=bien.Photo,
						HeightRequest=150,
						Margin = new Thickness(0,0,0,15)
					},
					new Label {
						Text="Description",
						FontAttributes=FontAttributes.Bold
					},
					new StackLayout {
						Orientation=StackOrientation.Horizontal,
						Children={
							new Label{
								Text=""+bien.Surface,
								FontSize=11
							},
							new Label {
								Text="m²",
								FontSize=11
							}
						}
					},
                    new StackLayout {
                        Orientation=StackOrientation.Horizontal,
                        Children={
                            new Label{
                                Text=""+bien.Prix,
                                FontSize=11
                            },
                            new Label {
                                Text="€",
                                FontSize=11,
                                Margin = new Thickness(0,0,0,15)
                            }
                        }
                    },
                    new Label {
						Text=bien.Description,
						Margin = new Thickness(0,0,0,15)
					},
					new Label {
						Text="Address",
						FontAttributes=FontAttributes.Bold,
						Margin = new Thickness(0,0,0,15)
					},
					new Label {
						Text=bien.Adresse
					},
					new StackLayout {
						Orientation=StackOrientation.Horizontal,
						Children={
							new Label {
								Text = bien.Ville
							},
							new Label {
								Text=bien.Cp,
								Margin = new Thickness(0,0,0,15)
							}
						}
					},
                    new Label
                    {
                        Text="Contact",
                        FontAttributes=FontAttributes.Bold,
                        Margin = new Thickness(0,0,0,15)
                    },
                    new Label
                    {
                        Text = bien.Telephone
                    },
                    new Label
                    {
                        Text = bien.Email,
						Margin = new Thickness(0,0,0,15)
                    },
					new Label
					{
						Text="Map",
						FontAttributes=FontAttributes.Bold,
						Margin = new Thickness(0,0,0,15)
					},
					map,
					button
				}
			};

			Content = new ScrollView
			{
				Content = stackLayout
			};
		}

		async void OnRetourClicked(object sender, EventArgs args)
		{
			await Navigation.PopModalAsync();
		}

		void OnFavClicked(object sender, EventArgs args)
		{
			bien.isFavorite = !bien.isFavorite;
			App.Database.SaveBienDetailed(bien);
		}

		async void On3DModelClicked(object sender, EventArgs e)
		{
			var showAlert = false;
			RequestGPSDto req = new RequestGPSDto();
			showAlert = await App.BienManager.CheckWs(req);

			if (!CrossConnectivity.Current.IsConnected)
				await DisplayAlert("No signal detected", "This feature only works if you can reach the Internet.", "OK");
			else if (showAlert){
				await DisplayAlert("Oops, our server seems to be down", "Please come back later, you will certainly be able to download the 3D model.", "OK");
			}
			else {
				var model = new ARPage(bien.Id);
				await Navigation.PushAsync(model);
			}
		}
	}
}
