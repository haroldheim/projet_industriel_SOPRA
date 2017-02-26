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

			architectView.SetLicenseKey("Q4kBzrx+yiK7nfOif1twHNL8YAm4841XuLbZQImySL0ZQUWmlCqJx+N7ahDi7AH3J2Zugus7xyhVXy9/KcYAUZf1Ws95jtuHe8ss/7OnTHzmVKkJSRRb+ZuSAb+7la1cYWXHrS5FylwlmYIhbeicAejQJn67V337QtNYvUC8kHpTYWx0ZWRfX0XKIa/cuUbHUuHtjqIKip/g1wFB5xty5HP0IRIxuPw7l1kKgjhTa4ZuUDvF44LqAjfX/sptwn8TKLUnhX7ooR2K2uCZSwHMhidPO1wlqv5hpbRxIiXWWujCTYFNXVgyK+31tt5a/IVXC2OfOrGFddvIiwwV9glVQvP5Bwb2KTuBmZP/cMI+0Ueb0NrDA3AkzpFv1DNDZuySwlDrJrJbjs2GeQTr6sKAX2UVO4dT4PHeCs/7sP1GY84tI42jW8UPsSFTnLS1TRUU6RaT6pwjm3dxQPzPb2uVBa0TsEhjUCHFTkDNDRpa7IvJPhkzdQ6klZdBhlq0PCXaaGD/VMc+1itOvo64xWPzPxIcxKMkWbc1NT48kVGI1Y/8ronYY8yGlsZ674QtP+/ExoXLxbSK/2IsRxffQxe46N+nXiXvVY4fml+B6NcBI4PQfyjlKFmXIG1DZ951lrwF5dcGh/FzanUK1gJwXHL7UJERMH5hvcLnI425R3jSKtc=");
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
