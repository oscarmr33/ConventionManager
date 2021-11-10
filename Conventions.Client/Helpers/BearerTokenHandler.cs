using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Convention.Client.Helpers
{
	public class BearerTokenHandler : DelegatingHandler
	{
		private readonly IHttpContextAccessor _httpContextAccesor;
		private readonly IHttpClientFactory _httpClientFactory;

		public BearerTokenHandler(IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
		{
			_httpContextAccesor = httpContextAccessor;
			_httpClientFactory = httpClientFactory;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var accessToken = await GetAccessTokenAsync();

			if (!string.IsNullOrWhiteSpace(accessToken))
			{
				request.SetBearerToken(accessToken);
			}

			return await base.SendAsync(request, cancellationToken);
		}

		public async Task<string> GetAccessTokenAsync()
		{
			var expiresAt = await _httpContextAccesor.HttpContext.GetTokenAsync("expires_at");
			var expiresAtDateTimeOffset = DateTimeOffset.Parse(expiresAt, CultureInfo.InvariantCulture);

			//verify if the token will expire in 60 seconds
			if((expiresAtDateTimeOffset.AddSeconds(-60)).ToUniversalTime() > DateTime.UtcNow)
			{
				return await _httpContextAccesor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);
			}

			var idpClient = _httpClientFactory.CreateClient("IDPClient");
			var discoveryResponse = await idpClient.GetDiscoveryDocumentAsync();

			var refreshToken = await _httpContextAccesor.HttpContext.GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

			var refreshResponse = await idpClient.RequestRefreshTokenAsync(new RefreshTokenRequest
			{
				Address = discoveryResponse.TokenEndpoint,
				ClientId = "conventionsclient",
				ClientSecret = "secret",
				RefreshToken = refreshToken
			});

			//Store the new tokens
			var updatedTokens = new List<AuthenticationToken>();
			updatedTokens.Add(new AuthenticationToken
			{
				Name = OpenIdConnectParameterNames.IdToken,
				Value = refreshResponse.IdentityToken
			});
			updatedTokens.Add(new AuthenticationToken
			{
				Name = OpenIdConnectParameterNames.AccessToken,
				Value = refreshResponse.AccessToken
			});
			updatedTokens.Add(new AuthenticationToken
			{
				Name = OpenIdConnectParameterNames.RefreshToken,
				Value = refreshResponse.RefreshToken
			});
			updatedTokens.Add(new AuthenticationToken
			{
				Name = "expires_at",
				Value = (DateTime.UtcNow + TimeSpan.FromSeconds(refreshResponse.ExpiresIn)).
						ToString("o", CultureInfo.InvariantCulture)
			});

			// get authenticate result, containing the current principal & 
			// properties
			var currentAuthenticateResult = await _httpContextAccesor
				.HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			// store the updated tokens
			currentAuthenticateResult.Properties.StoreTokens(updatedTokens);

			// sign in
			await _httpContextAccesor.HttpContext.SignInAsync(
				CookieAuthenticationDefaults.AuthenticationScheme,
				currentAuthenticateResult.Principal,
				currentAuthenticateResult.Properties);

			return refreshResponse.AccessToken;
		}
	}
}
