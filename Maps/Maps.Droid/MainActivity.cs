using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Support.V4.App;
using Plugin.Permissions;

namespace Maps.Droid
{
    [Activity(Label = "FindMyHome", Icon = "@drawable/icon", Theme = "@style/MainTheme", ScreenOrientation = ScreenOrientation.Portrait, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
		private static readonly int REQUEST_CAMERA = 0;

        protected override void OnCreate(Bundle bundle)
        {
			if (Android.Support.V4.Content.ContextCompat.CheckSelfPermission(this, Android.Manifest.Permission.Camera) != (int)Permission.Granted)
			{
				ActivityCompat.RequestPermissions(this, new String[] { Android.Manifest.Permission.Camera }, REQUEST_CAMERA);
			}

            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);


            global::Xamarin.Forms.Forms.Init(this, bundle);
            global::Xamarin.FormsMaps.Init(this, bundle);
            LoadApplication(new App());
        }

		public override void OnRequestPermissionsResult(int requestCode, string[] permissions, Permission[] grantResults)
		{
			PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
		}
    }
}

