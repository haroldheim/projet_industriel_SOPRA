using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(Maps.Droid.CustomTabbedRenderer))]
namespace Maps.Droid
{
	public class CustomTabbedRenderer : TabbedPageRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
		{
			base.OnElementChanged(e);

			var propInfo = typeof(TabbedPageRenderer).GetProperty("UseAnimations", BindingFlags.Instance | BindingFlags.NonPublic);
			propInfo.SetValue(this, false);
		}
	}
}
