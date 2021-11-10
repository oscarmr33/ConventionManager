using Convention.Client.Extension;
using Convention.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Convention.Client.Helpers
{
	public class UserProcesor
	{
		private readonly string _baseUrl = "https://localhost:44398/user";
		private readonly IHttpClientFactory _httpClientFactory;

		public UserProcesor(IHttpClientFactory clientFactory)
		{
			_httpClientFactory = clientFactory;
		}

		public async Task VerifyUserInApi(UserDto user)
		{
			var exists = await GetUser(user.Id);

			if(exists == null)
			{
				await CreateUser(user);
			}
		}

		public async Task<UserDto> GetUser(Guid id)
		{
			var getEndpoint = $"{_baseUrl}/{id}";
			var apiCaller = new ApiCaller(_httpClientFactory);
			var response = await apiCaller.Get<UserDto>(getEndpoint);

			return response;
		}

		public async Task CreateUser(UserDto user)
		{
			var apiCaller = new ApiCaller(_httpClientFactory);
			await apiCaller.Post(_baseUrl, user);
		}
	}
}
