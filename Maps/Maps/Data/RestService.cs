using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Maps
{
	public class RestService : IRestService
	{
		HttpClient client;

		public List<BienImmo> Biens { get; private set; }

		public RestService()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 25600;
		}

		public async Task<List<BienImmo>> RefreshDataAsync()
		{
			Biens = new List<BienImmo>();

			var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

			try
			{
				var response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					Biens = JsonConvert.DeserializeObject<List<BienImmo>>(content);
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(@"ERROR {0}", e.Message);
			}

			return Biens;
		}
	}
}
