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
        private readonly ConventionProcesor _procesor;
        private readonly UserProcesor _userProcesor;

        public ConventionsController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
            _procesor = new ConventionProcesor(httpClientFactory);
            _userProcesor = new UserProcesor(httpClientFactory);
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                var model = await _procesor.GetConventions();

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
            try
            {
                var model = await _procesor.GetConvention(id);

                return View(model);
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("AccessDenied", "Authorization");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize(Roles = "admin")]//Comma separate for more
        public ActionResult Create()
        {
            ViewBag.Message = "Create a new Convention";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles ="admin")]//Comma separate for more
        public async Task<ActionResult> Create(ConventionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _procesor.CreateConvention(model);
                }

                return Redirect("/Conventions");
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("AccessDenied", "Authorization");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [Authorize(Roles = "admin")]//Comma separate for more
        public async Task<ActionResult> Edit(Guid id)
        {
            try
            {
                var model = await _procesor.GetConvention(id);
                return View(model);
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("AccessDenied", "Authorization");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "admin")]//Comma separate for more
        public async Task<ActionResult> Edit(ConventionModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _procesor.UpdateConvention(model);
                }

                return Redirect("/Conventions");
            }
            catch (HttpRequestException e)
            {
                return RedirectToAction("AccessDenied", "Authorization");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task<ActionResult> Register(Guid id)
		{
            var userId = new Guid(User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value);
            await _userProcesor.RegisterToConvention(id, userId);
            return Redirect($"/Conventions/Details/{id}");
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
