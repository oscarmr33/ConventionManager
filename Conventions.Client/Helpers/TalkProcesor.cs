using Convention.Client.Extension;
using Convention.Client.Models;
using Convention.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace Convention.Client.Helpers
{
	public class TalkProcesor
	{
		private readonly string _baseUrl = "https://localhost:44398/talks";
		private readonly Locations _locations;
		private readonly IHttpClientFactory _httpClientFactory;

		public TalkProcesor(IHttpClientFactory clientFactory)
		{
			_httpClientFactory = clientFactory;
			_locations = new Locations(_httpClientFactory);
		}

		public async Task<TalkModel> GetTalk(Guid id)
		{
			var apiCaller = new ApiCaller(_httpClientFactory);
			string apiUrl = $"{_baseUrl}/{id}";
			var response = await apiCaller.Get<TalkDto>(apiUrl);
			var result = response.FromDto();

			if (response.LocationId != null)
			{
				var loc = await _locations.GetLocation(response.LocationId);
				result.Location = new LocationModel() { Id = loc.Id, Name = loc.Name };
			}

			return result;
		}

		public async Task<IEnumerable<TalkModel>> GetTalksByConvention(Guid id)
		{
			var url = $"{_baseUrl}/getbyconvention/{id}";
			var apiCaller = new ApiCaller(_httpClientFactory);
			var result = await apiCaller.Get<IEnumerable<TalkDto>>(url);
			var res = new List<TalkModel>();

			if (result != null && result.Count() > 0)
			{
				foreach (var talk in result)
				{
					var model = talk.FromDto();
					try
					{
						var loc = await _locations.GetLocation(talk.LocationId);
						model.Location = new LocationModel() { Id = loc.Id, Name = loc.Name };
					}
					catch
					{
						model.Location = new LocationModel();
					}
					res.Add(model);
				}
			}

			return res;
		}

		public async Task CreateTalk(CreateTalkModel talk)
		{
			var apiCaller = new ApiCaller(_httpClientFactory);
			await apiCaller.Post(_baseUrl, talk);
		}

		public async Task RegisterToTalk(Guid id, Guid userId)
		{
			//GET /talks/id?attendeeId
			var url = $"{_baseUrl}/register/{id}?attendeeId={userId}";
			var apiCaller = new ApiCaller(_httpClientFactory);
			await apiCaller.Get(url);
		}
	}
}