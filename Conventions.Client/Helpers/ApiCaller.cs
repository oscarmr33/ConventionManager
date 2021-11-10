using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
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

			var request = new HttpRequestMessage(
				HttpMethod.Get,
				$"{httpClient.BaseAddress}{url}");

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

		public void Post<T>(string url, T payload)
		{
			var httpRequest = (HttpWebRequest)WebRequest.Create(url);
			httpRequest.Method = "POST";
			httpRequest.Accept = "application/json";
			httpRequest.ContentType = "application/json";

			var data = JsonConvert.SerializeObject(payload);

			using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
			{
				streamWriter.Write(data);
			}

			var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				var result = streamReader.ReadToEnd();
			}
		}

		public void Put<T>(string url, T payload)
		{
			var httpRequest = (HttpWebRequest)WebRequest.Create(url);
			httpRequest.Method = "PUT";
			httpRequest.Accept = "application/json";
			httpRequest.ContentType = "application/json";

			var data = JsonConvert.SerializeObject(payload);

			using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
			{
				streamWriter.Write(data);
			}

			var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
			using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
			{
				var result = streamReader.ReadToEnd();
			}
		}
	}
}