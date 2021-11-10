using Convention.Client.Extension;
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
    public class TalksController : Controller
    {
        private readonly TalkProcesor _talkProcesor;

        public TalksController(IHttpClientFactory httpClientFactory)
        {     
            _talkProcesor = new TalkProcesor(httpClientFactory);
        }

        public async Task<IActionResult> TalksByConvention(Guid conventiondId)
        {
            try
            {
                var model = await _talkProcesor.GetTalksByConvention(conventiondId);

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
                var model = await _talkProcesor.GetTalk(id);

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

        public ActionResult Create(Guid id)
        {
            ViewBag.Message = "Create a new Talk";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(Guid id, CreateTalkModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    model.SpeakerId = new Guid(User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value);
                    model.ConventionId = id;
                    await _talkProcesor.CreateTalk(model);
                }

                return Redirect($"/Talks/TalksByConvention/{id}");
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
            await _talkProcesor.RegisterToTalk(id, userId);
            return Redirect($"/Talks/Details/{id}");
        }
    }
}
