﻿using Convention.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Convention.Client.Helpers
{
	public class Locations
	{
		private readonly string _baseUrl = "https://api.openbrewerydb.org/breweries";
		private readonly ApiCaller _apiCaller;
		//private readonly IHttpClientFactory _httpClientFactory;

		public Locations(IHttpClientFactory clientFactory)
		{
			_apiCaller = new ApiCaller(clientFactory);
		}

		public async Task<LocationModel> GetLocation(string id)
		{
			string apiUrl = $"{_baseUrl}/{id}";
			return await _apiCaller.Get<LocationModel>(apiUrl);
		}
	}
}