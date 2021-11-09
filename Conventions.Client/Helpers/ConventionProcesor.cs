using Convention.Client.Extension;
using Convention.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Convention.Client.Helpers
{
	public class ConventionProcesor
	{
		private readonly string _baseUrl = "https://localhost:44398/conventions";
		private readonly Locations _locations;

		public ConventionProcesor()
		{
			_locations = new Locations();
		}

		public async Task<ConventionModel> GetConvention(Guid id)
		{
			var apiCaller = new ApiCaller();
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
	}
}