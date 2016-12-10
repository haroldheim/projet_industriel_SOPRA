using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Maps
{
	public partial class SearchPage : ContentPage
	{
		Label label;
		public SearchPage()
		{
			Button b = new Button
			{
				Text = "Annuler",
				Margin = new Thickness(5, 0, 0, 0)
			};
			b.Clicked += OnAnnulerClicked;

			Button b2 = new Button
			{
				Text = "Valider"
			};
			b2.Clicked += OnValiderClicked;

			CustomCheckbox ccbMaison = new CustomCheckbox
			{
				Checked = false,
			};

			CustomCheckbox ccbAppartement = new CustomCheckbox
			{
				Checked = false,
			};

			var grid = new Grid { Margin = new Thickness(5, 10, 5, 10)};
			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			var topLeft = new Label { Text = "Minimum" };
			var topRight = new Label { Text = "Maximum" };
			var bottomLeft = new Entry { Placeholder="M²", Keyboard = Keyboard.Numeric };
			var bottomRight = new Entry { Placeholder = "M²", Keyboard = Keyboard.Numeric };
			grid.Children.Add(topLeft, 0, 0);
			grid.Children.Add(topRight, 1, 0);
			grid.Children.Add(bottomLeft, 0, 1);
			grid.Children.Add(bottomRight, 1, 1);

			var grid2 = new Grid { Margin = new Thickness(5, 10, 5, 10) };
			grid2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			grid2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			grid2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			grid2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			var topLeft2 = new Label { Text = "Minimum" };
			var topRight2 = new Label { Text = "Maximum" };
			var bottomLeft2 = new Entry { Placeholder = "M²", Keyboard = Keyboard.Numeric };
			var bottomRight2 = new Entry { Placeholder = "M²", Keyboard = Keyboard.Numeric };
			grid2.Children.Add(topLeft2, 0, 0);
			grid2.Children.Add(topRight2, 1, 0);
			grid2.Children.Add(bottomLeft2, 0, 1);
			grid2.Children.Add(bottomRight2, 1, 1);

			Slider slider = new Slider
			{
				Minimum = 0,
				Maximum = 10,
				Margin = new Thickness(20, 0, 20, 0),
				Value = 5
			};
			slider.ValueChanged += OnSliderValueChanged;

			label = new Label
			{
				Text = "5 km",
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			InitializeComponent();
			StackLayout stackLayout = new StackLayout
			{
				Spacing = 0,
				Children = {
					new StackLayout{
						Orientation= StackOrientation.Horizontal,
						Children={
							new SearchBar{
								WidthRequest=250,
								Placeholder="Adresse"
							},
							 b
						}
					},

					new Label {
						Text="Type de bien",
						FontAttributes=FontAttributes.Bold,
						Margin = new Thickness(5, 10, 5, 10)
					},
					new StackLayout{
						Orientation= StackOrientation.Horizontal,
						VerticalOptions = LayoutOptions.Center,
						HorizontalOptions = LayoutOptions.Center,
						Children={
							ccbAppartement,
							new Label {
								Text="Appartement",
								VerticalOptions = LayoutOptions.CenterAndExpand,
								XAlign = TextAlignment.End,
								YAlign = TextAlignment.Center,
								Margin = new Thickness(0, 0, 10, 0)
							},
							ccbMaison,
							new Label {
								Text="Maison",
								VerticalOptions = LayoutOptions.CenterAndExpand,
						XAlign = TextAlignment.End,
						YAlign = TextAlignment.Center
							}
						}
					},
					new Label {
						Text="Surface",
						FontAttributes=FontAttributes.Bold,
						Margin = new Thickness(5, 10, 5, 5)
					},
					grid,
					new Label {
						Text="Prix",
						FontAttributes=FontAttributes.Bold,
						Margin = new Thickness(5, 10, 5, 5)
					},
					grid2,
					new StackLayout{
						Orientation= StackOrientation.Horizontal,
						Children={
							new Label {
								Text="Aire de recherche",
								FontAttributes=FontAttributes.Bold,
								Margin = new Thickness(5, 10, 5, 10)
							},
							 label
						}
					},slider
					,
					b2
				}
			};
			Content = stackLayout;
		}

		async void OnAnnulerClicked(object sender, EventArgs args)
		{
			await Navigation.PopModalAsync();
		}

		async void OnValiderClicked(object sender, EventArgs args)
		{
			await Navigation.PopModalAsync();
		}

		void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
		{
			label.Text = String.Format("{0:F1} km", e.NewValue);
		}
	}
}
