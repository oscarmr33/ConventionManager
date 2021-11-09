using ConventionsClient.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace ConventionsClient.Helpers
{
	public class Locations
	{
		private readonly string _baseUrl = "https://api.openbrewerydb.org/breweries";
		private readonly ApiCaller _apiCaller;

		public Locations()
		{
			_apiCaller = new ApiCaller();
		}

		public async Task<LocationModel> GetLocation(string id)
		{
			string apiUrl = $"{_baseUrl}/{id}";
			return await _apiCaller.Get<LocationModel>(apiUrl);
		}
	}
}