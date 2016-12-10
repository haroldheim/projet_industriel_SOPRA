using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageRenderer))]
namespace Maps.Droid
{
	public class CustomTabbedPageRenderer : TabbedPageRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<TabbedPage> e)
		{

			var info = typeof(TabbedPageRenderer).GetTypeInfo();
			var fieldInfo = info.GetField("_useAnimations", BindingFlags.Instance | BindingFlags.NonPublic);
			fieldInfo.SetValue(this, false);
			base.OnElementChanged(e);
		}
	}
}
