﻿using System;
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

		public RestService()
		{
			client = new HttpClient();
			client.MaxResponseContentBufferSize = 25600;
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
	}
}