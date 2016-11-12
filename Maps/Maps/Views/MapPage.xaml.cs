using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace Maps
{
    public partial class MapPage : ContentPage
    {
        public MapPage()
        {
            InitializeComponent();

            MyMap.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(48.669075, 6.155275), Distance.FromMeters(500)));

            var pin = new Pin()
            {
                Position = new Position(48.669075, 6.155275),
                Label = "Ecole TELECOM Nancy"
            };

            MyMap.Pins.Add(pin);
        }
    }
}
