using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace Maps
{
	public class MapPageViewModel
	{
		public IEnumerable<BienImmoLight> BiensImmoLight { get; set; }

		public MapPageViewModel()
		{
		}
	}
}
