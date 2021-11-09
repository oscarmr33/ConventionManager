using Convention.Client.Helpers;
using Convention.Client.Models;
using Convention.Client.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Convention.Client.Controllers
{ 
    [Authorize]
    public class ConventionsController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly string _baseUrl = "https://localhost:44398/conventions";
        private readonly ConventionProcesor _procesor;

        public ConventionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _procesor = new ConventionProcesor();
        }

        public async Task<IActionResult> Index()
        {
            var apiCaller = new ApiCaller();
            var model = await apiCaller.Get<IEnumerable<ConventionModel>>(_baseUrl);

            return View(model);
        }

        public async Task<ActionResult> Details(Guid id)
        {
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

        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
        }

        public async Task WriteOutIdentityInformation()
        {
            //get the saved identity token
            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            //write it out
            Debug.WriteLine($"Identity toke: {identityToken}");

            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }
        }
    }
}
