using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Maps
{
	public class RestService : IRestService
	{
		HttpClient client;

		public List<BienImmoLight> Biens { get; private set; }
		public BienImmo Bien { get; set; }

		public RestService()
		{
			client = new HttpClient();
			TimeSpan span = new TimeSpan(0, 0, 0, 7, 0);
			client.Timeout = span;
			client.MaxResponseContentBufferSize = 25600;
		}

		public async Task<bool> CheckWs(RequestGPSDto requestGPSDto)
		{
			bool offline = false;
			var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

			try
			{
				var json = JsonConvert.SerializeObject(requestGPSDto);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;

				response = await client.PostAsync(uri, content);

				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					Biens = JsonConvert.DeserializeObject<List<BienImmoLight>>(result);
				}
			}
			catch (Exception e)
			{
				offline = true;
				Debug.WriteLine(@"ERROR {0}", e.Message);
			}

			return offline;
		}

		public async Task<List<BienImmoLight>> RefreshDataAsync(RequestGPSDto requestGPSDto)
		{
			Biens = new List<BienImmoLight>();
			var uri = new Uri(string.Format(Constants.RestUrl, string.Empty));

			try
			{
				var json = JsonConvert.SerializeObject(requestGPSDto);
				var content = new StringContent(json, Encoding.UTF8, "application/json");

				HttpResponseMessage response = null;

				response = await client.PostAsync(uri, content);

				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					Biens = JsonConvert.DeserializeObject<List<BienImmoLight>>(result);
				}
			}
			catch (Exception e)
			{
				Debug.WriteLine(@"ERROR {0}", e.Message);
			}

			return Biens;
		}

		public async Task<BienImmo> GetBienImmo(int id)
		{
			Bien = new BienImmo();

			var uri = new Uri(string.Format(Constants.RestUrlGetBiens + id, string.Empty));

			try
			{
				var response = await client.GetAsync(uri);
				if (response.IsSuccessStatusCode)
				{
					var content = await response.Content.ReadAsStringAsync();
					Bien = JsonConvert.DeserializeObject<BienImmo>(content);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}

			return Bien;
		}
	}
}
