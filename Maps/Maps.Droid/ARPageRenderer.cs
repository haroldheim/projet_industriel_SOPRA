﻿using System;
using Android.App;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(Maps.ARPage), typeof(Maps.Droid.ARPageRenderer))]
namespace Maps.Droid
{
	public class ARPageRenderer : PageRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Page> e)
		{
			base.OnElementChanged(e);

			var page = e.NewElement as ARPage;
			var activity = this.Context as Activity;
			var wikitudeActivity = new Intent(activity, typeof(WikitudeActivity));

			wikitudeActivity.PutExtra("id", page.worldId);
			wikitudeActivity.SetFlags(ActivityFlags.NoHistory);
			activity.StartActivity(wikitudeActivity);
			activity.OnBackPressed();
		}
	}
}
