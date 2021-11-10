using Convention.Client.Helpers;
using Convention.Client.Models;
using Convention.Client.ViewModels;
using IdentityModel.Client;
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
            _httpClientFactory = httpClientFactory;
            _procesor = new ConventionProcesor(httpClientFactory);
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var apiCaller = new ApiCaller(_httpClientFactory);
                var model = await apiCaller.Get<IEnumerable<ConventionModel>>("conventions");

                return View(model);
            }
            catch(HttpRequestException e)
			{
                return RedirectToAction("AccessDenied", "Authorization");
			}
            catch(Exception e)
			{
                throw e;
			}
        }

        public async Task<ActionResult> Details(Guid id)
        {
            var model = await _procesor.GetConvention(id);

            return View(model);
        }

        [Authorize(Roles = "admin")]//Comma separate for more
        public ActionResult Create()
        {
            ViewBag.Message = "Employee Sign Up";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="admin")]//Comma separate for more
        public ActionResult Create(ConventionModel model)
        {
            if (ModelState.IsValid)
            {
                var apiCaller = new ApiCaller(_httpClientFactory);
                apiCaller.Post(_baseUrl, model);
            }

            return Redirect("/Conventions");
        }

        [Authorize(Roles = "admin")]//Comma separate for more
        public async Task<ActionResult> Edit(Guid id)
        {
            var model = await _procesor.GetConvention(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]//Comma separate for more
        public ActionResult Edit(ConventionModel model)
        {
            if (ModelState.IsValid)
            {
                var apiCaller = new ApiCaller(_httpClientFactory);
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

        public async Task CreateUser()
		{
            var idpClient = _httpClientFactory.CreateClient("IDPClient");
            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();

            if(metaDataResponse.IsError)
			{
                throw new Exception("Problem accessing the discovery endpoint.", metaDataResponse.Exception);
			}

            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var userInfoResponse = await idpClient.GetUserInfoAsync(
                new UserInfoRequest { 
                    Address = metaDataResponse.UserInfoEndpoint,
                    Token = identityToken
                });

            if(userInfoResponse.IsError)
            {
                throw new Exception("Problem accessing the UserInfo endpoint.", metaDataResponse.Exception);
            }

            var address = userInfoResponse.Claims.FirstOrDefault(c => c.Type == "address")?.Value;
        }
    }
}
