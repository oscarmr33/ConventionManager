using Convention.Client.Extension;
using Convention.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Convention.Client.Helpers
{
	public class ConventionProcesor
	{
		private readonly string _baseUrl = "https://localhost:44398/conventions";
		private readonly Locations _locations;
		private readonly IHttpClientFactory _httpClientFactory;

		public ConventionProcesor(IHttpClientFactory clientFactory)
		{
			_httpClientFactory = clientFactory;
			_locations = new Locations(_httpClientFactory);
		}

		public async Task<ConventionModel> GetConvention(Guid id)
		{
			var apiCaller = new ApiCaller(_httpClientFactory);
			string apiUrl = $"{_baseUrl}/{id}";
			var response = await apiCaller.Get<ConventionDto>(apiUrl);
			var result = response.FromDto();

			if (response.LocationsId != null)
			{
				foreach (var location in response.LocationsId)
				{
					var loc = await _locations.GetLocation(location);
					result.Locations.Add(new LocationModel() { Id = loc.Id, Name = loc.Name });
				}
			}

			return result;
		}

		public async Task<IEnumerable<ConventionModel>> GetConventions()
		{
			var apiCaller = new ApiCaller(_httpClientFactory);
			return await apiCaller.Get<IEnumerable<ConventionModel>>(_baseUrl);
		}

		public async Task CreateConvention(ConventionModel convention)
		{
			var apiCaller = new ApiCaller(_httpClientFactory);
			await apiCaller.Post(_baseUrl, convention);
		}

		public async Task UpdateConvention(ConventionModel convention)
		{
			var apiCaller = new ApiCaller(_httpClientFactory);
			var url = $"{_baseUrl}/{convention.Id}";
			await apiCaller.Put(url, convention);
		}
	}
}