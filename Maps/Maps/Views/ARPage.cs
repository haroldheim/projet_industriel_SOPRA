using Xamarin.Forms;

namespace Maps
{
	public class ARPage : ContentPage
	{
		public int worldId;

		public ARPage(int worldId)
		{
			this.worldId = worldId;
			this.Title = "3D model";
		}
	}
}

