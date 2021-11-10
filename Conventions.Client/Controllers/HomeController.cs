using Convention.Client.Helpers;
using Convention.Client.Models;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Convention.Client.Controllers
{
    [Authorize]
	public class HomeController : Controller
	{
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserProcesor _userProcesor;

        public HomeController(IHttpClientFactory httpClientFactory)
		{
            _httpClientFactory = httpClientFactory;
            _userProcesor = new UserProcesor(httpClientFactory);
		}

        public async Task<ActionResult> Index()
		{
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    await _userProcesor.VerifyUserInApi(GetUserFromAuth());
                }
            }
            catch (HttpRequestException)
            {
                return RedirectToAction("AccessDenied", "Authorization");
            }
            catch (Exception e)
            {
                throw e;
            }

            return View();
		}

        public async Task CreateUser()
        {
            var idpClient = _httpClientFactory.CreateClient("IDPClient");
            var metaDataResponse = await idpClient.GetDiscoveryDocumentAsync();

            if (metaDataResponse.IsError)
            {
                throw new Exception("Problem accessing the discovery endpoint.", metaDataResponse.Exception);
            }

            var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            var userInfoResponse = await idpClient.GetUserInfoAsync(
                new UserInfoRequest
                {
                    Address = metaDataResponse.UserInfoEndpoint,
                    Token = identityToken
                });

            if (userInfoResponse.IsError)
            {
                throw new Exception("Problem accessing the UserInfo endpoint.", metaDataResponse.Exception);
            }

            var address = userInfoResponse.Claims.FirstOrDefault(c => c.Type == "address")?.Value;
        }

        public UserDto GetUserFromAuth()
		{
            return new UserDto()
            {
                Id = new Guid(User.Claims.FirstOrDefault(c => c.Type == "sub")?.Value),
                FirstName = $"{User.Claims.FirstOrDefault(c => c.Type == "given_name")?.Value}",
                LastName = $"{User.Claims.FirstOrDefault(c => c.Type == "family_name")?.Value}",
                Email = User.Claims.FirstOrDefault(c => c.Type == "email")?.Value,
                Telephone = User.Claims.FirstOrDefault(c => c.Type == "telephone")?.Value
            };
        }
    }
}
