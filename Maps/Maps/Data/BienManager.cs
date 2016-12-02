using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Maps
{
	public class BienManager
	{
		IRestService restService;

		public BienManager(IRestService service)
		{
			restService = service;
		}

		public Task<List<BienImmoLight>> GetTaskAsync(RequestGPSDto req)
		{
			return restService.RefreshDataAsync(req);
		}

	}
}
