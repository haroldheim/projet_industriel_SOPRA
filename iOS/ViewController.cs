using System;

using UIKit;
using Mapbox;
using System.Linq;
using CoreLocation;
using Foundation;

namespace MapBoxSampleiOS
{
    public partial class ViewController : UIViewController, IMapViewDelegate
    {
        public ViewController (IntPtr handle) : base (handle)
        {
        }

        MapView mapView;

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			// Create a MapView and set the coordinates/zoom
			mapView = new MapView(View.Bounds);
			mapView.SetCenterCoordinate(new CLLocationCoordinate2D(48.665, 6.1603), 11, false);

			mapView.AddAnnotation(new PointAnnotation
			{
				Coordinate = new CLLocationCoordinate2D(48.665, 6.1603),
				Title = "Marqueur test",
				Subtitle = "Texte test"
			});

			// Set ourselves as the delegate
			mapView.Delegate = this;

			View.AddSubview(mapView);

		}

        public override void DidReceiveMemoryWarning ()
        {
            base.DidReceiveMemoryWarning ();

            // Cleanup anything possible
            mapView.EmptyMemoryCache ();
        }
    }
}

