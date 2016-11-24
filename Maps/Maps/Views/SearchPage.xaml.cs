using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;

namespace Maps
{
	public partial class SearchPage : ContentPage
	{
		public SearchPage()
		{
			InitializeComponent();
			Content = new TableView
			{
				Root = new TableRoot("")
				{
					new TableSection("Caractéristiques principales")
					{
						new EntryCell
						{
							Label = "Bien",
							Placeholder = "Maison/Appartement/Les deux",
							Keyboard = Keyboard.Text
						},
						new EntryCell
						{
							Label = "Acquisition",
							Placeholder = "Achat/Location/Les deux",
							Keyboard = Keyboard.Numeric
						}
					},
					new TableSection("Surface")
					{
						new EntryCell
						{
							Label = "Minimum",
							Placeholder = "m²",
							Keyboard = Keyboard.Numeric
						},
						new EntryCell
						{
							Label = "Maximum",
							Placeholder = "m²",
							Keyboard = Keyboard.Numeric
						}
					},
					new TableSection("Aire de recherche")
					{
						new ViewCell
						{
							View = new StackLayout
							{
								Orientation = StackOrientation.Horizontal,
								Children =
								{
									new Slider
									{
										Minimum = 0,
										Maximum = 500,
										HorizontalOptions = LayoutOptions.FillAndExpand
									}
								}
							}
						}
					},
					new TableSection("Prix")
					{
						new EntryCell
						{
							Label = "Minimum",
							Placeholder = "€",
							Keyboard = Keyboard.Numeric
						},
						new EntryCell
						{
							Label = "Maximum",
							Placeholder = "€",
							Keyboard = Keyboard.Numeric
						}
					}
				}
			};
		}
	}
}
