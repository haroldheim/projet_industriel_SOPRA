using System;
using Xamarin.Forms;

namespace Maps
{
    public class MenuPage : ContentPage
    {
        public MenuPage()
        {

            var openMapButton = new Button { Text = "Open the map" };
            openMapButton.Clicked += OnMapPageClicked;
            var favoritesButton = new Button { Text = "Favorites homes" };
            var searchHistoryButton = new Button { Text = "Search history" };

            Title = "Find a home";
            Content = new StackLayout
            {
                Margin = new Thickness(0, 20, 0, 0),
                Children =
                {
                        openMapButton,
                        favoritesButton,
                        searchHistoryButton
                }
            };

        }

        async void OnMapPageClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MapPage());
        }
    }
}
