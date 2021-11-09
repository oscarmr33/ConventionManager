using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Convention.Client.Models
{
	public class LocationModel
	{
		public string Id { get; set; }
		public string Name { get; set; }
	}
}

/*
 * "id": "10-56-brewing-company-knox",
"name": "10-56 Brewing Company",
"brewery_type": "micro",
"street": "400 Brown Cir",
"address_2": null,
"address_3": null,
"city": "Knox",
"state": "Indiana",
"county_province": null,
"postal_code": "46534",
"country": "United States",
"longitude": "-86.627954",
"latitude": "41.289715",
"phone": "6308165790",
"website_url": null,
"updated_at": "2021-10-23T02:24:55.243Z",
"created_at": "2021-10-23T02:24:55.243Z"**/