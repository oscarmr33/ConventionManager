using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Convention.Client.Helpers
{
	public class BearerTokenHandler : DelegatingHandler
	{
		private readonly IHttpContextAccessor _httpContextAccesor;

		public BearerTokenHandler(IHttpContextAccessor httpContextAccessor)
		{
			_httpContextAccesor = httpContextAccessor;
		}

		protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
		{
			var accessToken = await _httpContextAccesor
			   .HttpContext.GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

			if (!string.IsNullOrWhiteSpace(accessToken))
			{
				request.SetBearerToken(accessToken);
			}

			return await base.SendAsync(request, cancellationToken);
		}
	}
}
