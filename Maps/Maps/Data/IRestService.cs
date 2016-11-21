using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maps
{
	public interface IRestService
	{
		Task<List<BienImmo>> RefreshDataAsync();

	}
}
