﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Maps
{
    public class MapPage : ContentPage
    {
        Map map;

        public MapPage()
        {
            map = new Map
            {
                HeightRequest = 100,
                WidthRequest = 960,
                VerticalOptions = LayoutOptions.FillAndExpand
            };

            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(48.67, 6.15), Distance.FromMeters(10)));

            var pin = new Pin()
            {
                Position = new Position(48.66, 6.15),
                Label = "TELECOM Nancy"
            };

            map.Pins.Add(pin);

            var stack = new StackLayout { Spacing = 0 };
            stack.Children.Add(map);
            Content = stack;
        }
       
    }
}