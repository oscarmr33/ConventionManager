using Conventions.Models.Dto;
using Conventions.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using System.Net.Http.Headers;

namespace Conventions.Client.Pages
{
    public class ConventionsModel : PageModel
    {
        private readonly ILogger<ConventionsModel> _logger;
        private HttpClient client = new HttpClient();
        string baseUrl = "https://localhost:44398/conventions";

        public ConventionsModel(ILogger<ConventionsModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            //ViewData["conventions"] = GetProductAsync(baseUrl).Result.Select(conv => new Item);
        }

        public async Task<IEnumerable<ConventionDto>> GetProductAsync(string path)
        {
            List<ConventionDto> conventions = null;
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
				conventions = (List<ConventionDto>) await response.Content.ReadAsAsync<IEnumerable<ConventionDto>>();
            }
			return conventions;
        }
    }
}
