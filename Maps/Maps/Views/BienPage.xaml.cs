using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Maps
{
	public partial class BienPage : ContentPage
	{
		FavoriteCheck favorites;

		public BienPage(BienImmo bien)
		{
			InitializeComponent();

			favorites = new FavoriteCheck
			{
				Checked = true
			};

			StackLayout stackLayout = new StackLayout
			{
				Spacing = 0,
				Padding = 5,
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
					}
				}
			};
			Content = stackLayout;
		}

		async void OnRetourClicked(object sender, EventArgs args)
		{

			await Navigation.PopModalAsync();
		}
	}
}
