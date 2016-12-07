using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maps
{
	public interface IRestService
	{
		Task<List<BienImmoLight>> RefreshDataAsync(RequestGPSDto requestGPSDto);
		Task<BienImmo> GetBienImmo(int id);
	}
}
