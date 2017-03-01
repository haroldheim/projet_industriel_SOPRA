using System;
using Foundation;
using UIKit;
using Wikitude.Architect;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Maps.ARPage), typeof(Maps.iOS.ARPageRenderer))]
namespace Maps.iOS
{
	public class ARPageRenderer : PageRenderer
	{
		int worldId;
		WTArchitectView architectView;
		WTNavigation navigation;
		ExampleArchitectViewDelegate architectViewDelegate = new ExampleArchitectViewDelegate();

		protected override void OnElementChanged(VisualElementChangedEventArgs e)
		{
			base.OnElementChanged(e);
			var page = e.NewElement as ARPage;
			worldId = page.worldId;
			this.Title = "3d model";
		}

		public override void ViewDidLoad()
		{
			base.ViewDidLoad();

			this.architectView = new WTArchitectView();
			this.architectView.Delegate = architectViewDelegate;
			this.View.AddSubview(this.architectView);
			this.architectView.TranslatesAutoresizingMaskIntoConstraints = false;

			NSDictionary views = new NSDictionary(new NSString("architectView"), architectView);
			this.View.AddConstraints(NSLayoutConstraint.FromVisualFormat("|[architectView]|", 0, null, views));
			this.View.AddConstraints(NSLayoutConstraint.FromVisualFormat("V:|[architectView]|", 0, null, views));

			architectView.SetLicenseKey("yV/CuS7fMQAsiu/ZMgUghXHxQKuo+5vrUyyFDUIk5BTym1hTKn3hRpg9eHeGsiq1pVKY9IMzdFAEJWqRGILrrQmhlLd+pGg+eDF72aRUom90xl80UnO6vrSGMRkS/pmrnM6EjN1464bGMwNNxn+WnncBDtCb+ycfIJUQZEu8hkNTYWx0ZWRfXyhu76iZIxT/SWUY/J8HJjafZnIf7vSJdTo6hyM8wgfSbW72gf+3n8VAnIw4J8MIFbAS1xyTLmrmpvw/RoOMmKaZZAeAQkTZz7Gtv0Ja9glyWlMjcEH4OhM2CrjetjX2jC2pMbqQpshFEmP2dB8KdOq3t2KBZ3NPodklpdK/+g9NIl2P+tQzN0fZitpYCflfqfyopQ3iiZeEZMen3P9LUcgvBoTgo49jNM6wYy+06RoW8dymZmAPiiKb2PKbCNtXJ3AjJSyJm610SdikGG4XOs6j00X3ryg0TtIZHj6veAcprIQBChHUBBWxTY4KyyMD1caXrFUBBi3m348k631N57ogheBBJA7FEpvP5pAgjuLTiMz20At4w7sbCK+on8XpEPzp0jTvMcGwRtamYQhVXfG3ApH1i0awC7LpFx2mx0ropZuOw/YEB9DrdaLlrfHuJ6OuqDEfTie1NMjFIFG4kGGgcf3j6GYOPpdhWH1wATf0PhUBcGMWscqegOPvh+woG20sTf08uG1tF0jPB/vCxHOJ4XOByyGucG4OV5BqjupXi4Kp7CxIJl0=\n");
			NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.DidBecomeActiveNotification, (notification) =>
			{
				if (navigation.Interrupted)
				{
					architectView.reloadArchitectWorld();
				}
				StartAR();
			});

			NSNotificationCenter.DefaultCenter.AddObserver(UIApplication.WillResignActiveNotification, (notification) =>
			{
				StopAR();
			});

			var path = NSBundle.MainBundle.BundleUrl.AbsoluteString + "3DModel/index.html?id=" + worldId;
			navigation = architectView.LoadArchitectWorldFromURL(NSUrl.FromString(path), WTFeatures.Geo);
		}

		public override void ViewWillAppear(bool animated)
		{
			base.ViewWillAppear(animated);
			StartAR();
		}

		public override void ViewDidDisappear(bool animated)
		{
			base.ViewDidDisappear(animated);
			StopAR();
		}

		public void StartAR()
		{
			if (!architectView.IsRunning)
			{
				architectView.Start((startupConfiguration) =>
			   {
				   // use startupConfiguration.CaptureDevicePosition = AVFoundation.AVCaptureDevicePosition.Front; to start the Wikitude SDK with an active front cam
				   startupConfiguration.CaptureDevicePosition = AVFoundation.AVCaptureDevicePosition.Back;
			   }, (isRunning, error) =>
			   {
				   if (isRunning)
				   {
					   Console.WriteLine("Wikitude SDK version " + WTArchitectView.SDKVersion + " is running.");
				   }
				   else
				   {
					   Console.WriteLine("Unable to start Wikitude SDK. Error: " + error.LocalizedDescription);
				   }
			   });
			}
		}

		public void StopAR()
		{
			if (architectView.IsRunning)
			{
				architectView.Stop();
			}
		}

		#region Rotation

		public override void WillRotate(UIInterfaceOrientation toInterfaceOrientation, double duration)
		{
			base.WillRotate(toInterfaceOrientation, duration);

			architectView.SetShouldRotateToInterfaceOrientation(true, toInterfaceOrientation);
		}

		public override bool ShouldAutorotate()
		{
			return true;
		}

		#endregion
	}
}
