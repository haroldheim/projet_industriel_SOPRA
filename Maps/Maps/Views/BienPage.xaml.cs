using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace Maps
{
	public partial class BienPage : ContentPage
	{
		FavoriteCheck favorites;
		BienImmo bien;
		Button button;

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
			var model = new ARPage("3_3dModels_3_Interactivity");
			await Navigation.PushAsync(model);
		}
	}
}
