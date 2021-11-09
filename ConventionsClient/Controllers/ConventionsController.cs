using ConventionsClient.Helpers;
using ConventionsClient.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace ConventionsClient.Controllers
{
	public class ConventionsController : Controller
	{
		private readonly string _baseUrl = "https://localhost:44398/conventions";
		private readonly ConventionProcesor _procesor;

		public ConventionsController()
		{
			_procesor = new ConventionProcesor();
		}

		public async Task<ActionResult> Index()
		{
			var apiCaller = new ApiCaller();
			var model = await apiCaller.Get<IEnumerable<ConventionModel>>(_baseUrl);

			return View(model);
		}

		public async Task<ActionResult> Details(Guid id)
		{
			//var apiCaller = new ApiCaller();
			//string apiUrl = $"{_baseUrl}/{id}";
			//var model = await apiCaller.Get<ConventionModel>(apiUrl);

			var model = await _procesor.GetConvention(id);

			return View(model);
		}

		public ActionResult Create()
		{
			ViewBag.Message = "Employee Sign Up";

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(ConventionModel model)
		{
			if (ModelState.IsValid)
			{
				var apiCaller = new ApiCaller();
				apiCaller.Post(_baseUrl, model);
			}

			return Redirect("/Conventions");
		}

		public async Task<ActionResult> Edit(Guid id)
		{
			var model = await _procesor.GetConvention(id);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(ConventionModel model)
		{
			if (ModelState.IsValid)
			{
				var apiCaller = new ApiCaller();
				var url = $"{_baseUrl}/{model.Id}";
				apiCaller.Put(url, model);
			}

			return Redirect("/Conventions");
		}
	}
}