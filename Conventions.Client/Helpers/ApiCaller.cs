using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Convention.Client.Helpers
{
	public class ApiCaller
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public ApiCaller(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<T> Get<T>(string url)
		{
			T res = default(T);

			var httpClient = _httpClientFactory.CreateClient("ConventionsApi");

			var request = new HttpRequestMessage(HttpMethod.Get, url);

			var response = await httpClient.SendAsync(
				request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				res = JsonConvert.DeserializeObject<T>(data);
			}
			else if(response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
			{
				throw new HttpRequestException("Access to the Api denied");
			}

			return res;
		}

		public async Task Get(string url)
		{
			var httpClient = _httpClientFactory.CreateClient("ConventionsApi");

			var request = new HttpRequestMessage(HttpMethod.Get, url);

			var response = await httpClient.SendAsync(
				request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

			if (response.IsSuccessStatusCode)
			{
				var data = await response.Content.ReadAsStringAsync();
				var res = JsonConvert.DeserializeObject<string>(data);
			}
			else if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
			{
				throw new HttpRequestException("Access to the Api denied");
			}
		}

		public async Task Post<T>(string url, T payload)
		{
			var httpClient = _httpClientFactory.CreateClient("ConventionsApi");

			var request = new HttpRequestMessage(HttpMethod.Post, url);
			var data = JsonConvert.SerializeObject(payload);
			request.Content = new StringContent(data, Encoding.UTF8, "application/json");

			var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

			if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
			{
				throw new HttpRequestException("Access to the Api denied");
			}
		}

		public async Task Put<T>(string url, T payload)
		{		
			var httpClient = _httpClientFactory.CreateClient("ConventionsApi");

			var request = new HttpRequestMessage(HttpMethod.Put, url);
			var data = JsonConvert.SerializeObject(payload);
			request.Content = new StringContent(data, Encoding.UTF8, "application/json");

			var response = await httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead).ConfigureAwait(false);

			if (response.StatusCode == HttpStatusCode.Unauthorized || response.StatusCode == HttpStatusCode.Forbidden)
			{
				throw new HttpRequestException("Access to the Api denied");
			}
		}
	}
}