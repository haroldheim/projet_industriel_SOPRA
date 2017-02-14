﻿using System;
using Android.App;
using Android.Util;
using Android.OS;
using Android.Widget;
using Java.IO;
using Wikitude.Architect;
using Wikitude.Tools.Device.Features;
using Wikitude.Common.Camera;

namespace Maps.Droid
{

	[Activity(Label = "WikitudeActivity")]
	public class WikitudeActivity : Activity, ArchitectView.IArchitectUrlListener
	{
		ArchitectView architectView;
		string worldUrl;

		protected override void OnCreate(Bundle bundle)
		{
			base.OnCreate(bundle);

			SetContentView(Resource.Layout.sample_cam);

			Title = Intent.GetStringExtra("id");

			worldUrl = "Wikitude" + File.Separator + Intent.GetStringExtra("id") + File.Separator + "index.html";

			architectView = FindViewById<ArchitectView>(Resource.Id.architectView);
			ArchitectStartupConfiguration startupConfiguration = new ArchitectStartupConfiguration();
			startupConfiguration.setLicenseKey(Constants.WIKITUDE_SDK_KEY);
			startupConfiguration.setFeatures(ArchitectStartupConfiguration.Features.Tracking2D);
			startupConfiguration.setCameraResolution(CameraSettings.CameraResolution.Auto);

			/* use  
			   int requiredFeatures = StartupConfiguration.Features.Tracking2D | StartupConfiguration.Features.Geo;
			   if you need both 2d Tracking and Geo
			*/
			int requiredFeatures = ArchitectStartupConfiguration.Features.Tracking2D;
			MissingDeviceFeatures missingDeviceFeatures = ArchitectView.isDeviceSupported(this, requiredFeatures);


			if ((ArchitectView.getSupportedFeaturesForDevice(Android.App.Application.Context) & requiredFeatures) == requiredFeatures)
			{
				architectView.OnCreate(startupConfiguration);
				architectView.RegisterUrlListener(this);
			}
			else {
				architectView = null;
				Toast.MakeText(this, missingDeviceFeatures.getMissingFeatureMessage(), ToastLength.Long).Show();

				//StartActivity(typeof(ErrorActivity));
			}
		}

		protected override void OnResume()
		{
			base.OnResume();

			if (architectView != null)
				architectView.OnResume();
		}

		protected override void OnPause()
		{
			base.OnPause();

			if (architectView != null)
				architectView.OnPause();
		}

		protected override void OnStop()
		{
			base.OnStop();
		}

		protected override void OnDestroy()
		{
			base.OnDestroy();

			if (architectView != null)
			{
				architectView.OnDestroy();
			}
		}

		public override void OnLowMemory()
		{
			base.OnLowMemory();

			if (architectView != null)
				architectView.OnLowMemory();
		}

		protected override void OnPostCreate(Bundle savedInstanceState)
		{
			base.OnPostCreate(savedInstanceState);

			if (architectView != null)
			{
				architectView.OnPostCreate();

				try
				{
					architectView.Load(worldUrl);
				}
				catch (Exception ex)
				{
					Log.Error("WIKITUDE_SAMPLE", ex.ToString());
				}
			}
		}

		public bool UrlWasInvoked(string url)
		{
			/* This is a example implementation of the UrlWasInvoked method */
			/* The url is defined in JS where document.location = 'architectsdk://...'; is used. */
			//Console.WriteLine("architect view invoked url: " + url);
			return true;
		}
	}
}
