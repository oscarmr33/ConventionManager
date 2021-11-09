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
		public async Task<T> Get<T>(string url)
		{
			T res = default(T);

			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri(url);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await client.GetAsync(url);
				if (response.IsSuccessStatusCode)
				{
					var data = await response.Content.ReadAsStringAsync();
					res = JsonConvert.DeserializeObject<T>(data);
				}
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