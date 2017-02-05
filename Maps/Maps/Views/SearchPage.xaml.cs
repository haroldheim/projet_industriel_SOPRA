using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Maps.Helpers;
using Xamarin.Forms;

namespace Maps
{
	public partial class SearchPage : ContentPage
	{
		Label label;
		double StepValue;
		Slider SliderMain;
		Entry bottomLeft;
		Entry bottomRight;
		Entry bottomLeft2;
		Entry bottomRight2;
		CustomCheckbox ccbMaison;
		CustomCheckbox ccbAppartement;
        CustomCheckbox ccbSale;
        CustomCheckbox ccbRental;

		public SearchPage()
		{
			Button b = new Button
			{
				Text = "Cancel",
				Margin = new Thickness(5, 0, 0, 0)
			};
			b.Clicked += OnAnnulerClicked;

			Button b2 = new Button
			{
				Text = "Apply"
			};
			b2.Clicked += OnValiderClicked;

			ccbRental = new CustomCheckbox
			{
				Checked = Settings.isRental
			};

			ccbSale = new CustomCheckbox
			{
				Checked = Settings.isSale
			};


			ccbMaison = new CustomCheckbox
			{
				Checked = Settings.isMaison
			};

			ccbAppartement = new CustomCheckbox
			{
				Checked = Settings.isAppartement
			};

			var grid = new Grid { Margin = new Thickness(5, 10, 5, 10) };
			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
			var topLeft = new Label { Text = "Minimum" };
			var topRight = new Label { Text = "Maximum" };
			bottomLeft = new Entry { Placeholder = "M²", Keyboard = Keyboard.Numeric, Text = "" + Settings.surfaceMin };
			bottomRight = new Entry { Placeholder = "M²", Keyboard = Keyboard.Numeric, Text = "" + Settings.surfaceMax };
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
			bottomLeft2 = new Entry { Placeholder = "M²", Keyboard = Keyboard.Numeric, Text = "" + Settings.prixMin };
			bottomRight2 = new Entry { Placeholder = "M²", Keyboard = Keyboard.Numeric, Text = "" + Settings.prixMax };
			grid2.Children.Add(topLeft2, 0, 0);
			grid2.Children.Add(topRight2, 1, 0);
			grid2.Children.Add(bottomLeft2, 0, 1);
			grid2.Children.Add(bottomRight2, 1, 1);

			StepValue = 1.0;

			SliderMain = new Slider
			{
				Maximum = 10.0f,
				Minimum = 1.0f,
				Value = Settings.aireRecherche,
				Margin = new Thickness(20, 0, 20, 0)
			};
			SliderMain.ValueChanged += OnSliderValueChanged;

			label = new Label
			{
				Text = Settings.aireRecherche + " km",
				HorizontalOptions = LayoutOptions.Center,
				VerticalOptions = LayoutOptions.CenterAndExpand
			};

			InitializeComponent();
			StackLayout stackLayout = new StackLayout
			{
				Spacing = 0,
				Children = {
					b,
					new Label
					{
						Text="Type of purchase",
						FontAttributes=FontAttributes.Bold,
						Margin = new Thickness(5,10,5,10)
					},
					new StackLayout{
						Orientation= StackOrientation.Horizontal,
						VerticalOptions = LayoutOptions.Center,
						HorizontalOptions = LayoutOptions.Center,
						Children={
							ccbRental,
							new Label {
								Text="Rental",
								VerticalOptions = LayoutOptions.CenterAndExpand,
								HorizontalTextAlignment = TextAlignment.End,
								VerticalTextAlignment = TextAlignment.Center,
								Margin = new Thickness(0, 0, 10, 0)
							},
							ccbSale,
							new Label {
								Text="Sale",
								VerticalOptions = LayoutOptions.CenterAndExpand,
								HorizontalTextAlignment = TextAlignment.End,
								VerticalTextAlignment = TextAlignment.Center
							}
						}
					},

					new Label {
						Text="Type of property",
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
								Text="Apartment",
								VerticalOptions = LayoutOptions.CenterAndExpand,
								HorizontalTextAlignment = TextAlignment.End,
								VerticalTextAlignment = TextAlignment.Center,
								Margin = new Thickness(0, 0, 10, 0)
							},
							ccbMaison,
							new Label {
								Text="House",
								VerticalOptions = LayoutOptions.CenterAndExpand,
								HorizontalTextAlignment = TextAlignment.End,
								VerticalTextAlignment = TextAlignment.Center
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
						Text="Price",
						FontAttributes=FontAttributes.Bold,
						Margin = new Thickness(5, 10, 5, 5)
					},
					grid2,
					new StackLayout{
						Orientation= StackOrientation.Horizontal,
						Children={
							new Label {
								Text="Search area",
								FontAttributes=FontAttributes.Bold,
								Margin = new Thickness(5, 10, 5, 10)
							},
							 label
						}
					},SliderMain
					,
					b2
				}
			};
			Content = new ScrollView
			{
				Content = stackLayout
			};
		}

		async void OnAnnulerClicked(object sender, EventArgs args)
		{
			await Navigation.PopModalAsync();
		}

		async void OnValiderClicked(object sender, EventArgs args)
		{
			Settings.aireRecherche = SliderMain.Value;
			Settings.surfaceMin = Double.Parse(bottomLeft.Text);
			Settings.surfaceMax = Double.Parse(bottomRight.Text);
			Settings.prixMin = Double.Parse(bottomLeft2.Text);
			Settings.prixMax = Double.Parse(bottomRight2.Text);
			Settings.isMaison = (bool)ccbMaison.Checked;
			Settings.isAppartement = (bool)ccbAppartement.Checked;
			Settings.isRental = (bool)ccbRental.Checked;
			Settings.isSale = (bool)ccbSale.Checked;
			Settings.isModified = true;
			await Navigation.PopModalAsync();
		}

		void OnSliderValueChanged(object sender, ValueChangedEventArgs e)
		{
			var newStep = Math.Round(e.NewValue / StepValue);

			SliderMain.Value = newStep * StepValue;

			label.Text = String.Format("{0} km", SliderMain.Value);
		}
	}
}
