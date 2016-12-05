using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Maps
{
	public class MapPageViewModel
	{
		public IEnumerable<BienImmoLight> BiensImmoLight { get; set; }

		public MapPageViewModel()
		{
			BiensImmoLight = App.Database.GetBiensLight();
		}
	}
}
