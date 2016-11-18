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
				Intent = TableIntent.Form,
				Root = new TableRoot("")
				{
					new TableSection ("What kind of home are you searching for")
					{
						new TextCell
						{
							Text = "TextCell Text",
							 Detail = "TextCell Detail"
						},
						new EntryCell {
							Label = "EntryCell:",
							Placeholder = "default keyboard",
							Keyboard = Keyboard.Default
						}
					},
					new TableSection ("Surface Area") {
						new EntryCell {
							Label = "Another EntryCell:",
							Placeholder = "phone keyboard",
							Keyboard = Keyboard.Telephone
						},
						new SwitchCell {
							Text = "SwitchCell:"
						}
					}
				}
			};
		}
	}
}
