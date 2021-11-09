using ConventionsClient.Helpers;
using ConventionsClient.Models;
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
using System.Web.Mvc;

namespace ConventionsClient.Controllers
{
	public class HomeController : Controller
	{
		public ActionResult Index()
		{
			return View();
		}

		public ActionResult About()
		{
			ViewBag.Message = "Your application description page.";

			return View();
		}

		public async Task<ActionResult> Contact()
		{
			ViewBag.Message = "Your contact page.";

			string apiUrl = "https://localhost:44398/conventions";

			using (HttpClient client = new HttpClient())
			{
				client.BaseAddress = new Uri(apiUrl);
				client.DefaultRequestHeaders.Accept.Clear();
				client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

				HttpResponseMessage response = await client.GetAsync(apiUrl);
				if (response.IsSuccessStatusCode)
				{
					var data = await response.Content.ReadAsStringAsync();
					var table = Newtonsoft.Json.JsonConvert.DeserializeObject<IEnumerable<ConventionModel>>(data);
				}


			}

			return View();
		}

		public async Task<ActionResult> Conventions()
		{
			var apiCaller = new ApiCaller();
			string apiUrl = "https://localhost:44398/conventions";
			var model = await apiCaller.Get<List<ConventionModel>>(apiUrl);

			return View(model);
		}

		public ActionResult ConventionCreate()
		{
			ViewBag.Message = "Employee Sign Up";

			return View();
		}

		[HttpPost]		
		public ActionResult ConventionCreate(ConventionModel model)
		{
			if (ModelState.IsValid)
			{
				string apiUrl = "https://localhost:44398/conventions";
				var httpRequest = (HttpWebRequest)WebRequest.Create(apiUrl);
				httpRequest.Method = "POST";
				httpRequest.Accept = "application/json";
				httpRequest.ContentType = "application/json";

				var data = JsonConvert.SerializeObject(model);

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

			return View();
		}
	}
}