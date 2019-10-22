using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Core.Entities;
using TerritoriesAssignment.Database;
using TerritoriesAssignment.WebApp.Models;
using TerritoriesAssignment.WebApp.Utilities;

namespace TerritoriesAssignment.WebApp.Controllers {
	[Route("api/country")]
	[ApiController]
	public class CountryController : Controller, IStorageController<CountryView> {
		public IDataStorage Storage { get; }

		public CountryController(IDataStorage storage) {
			Storage = storage;
		}
		[HttpGet("getItems")]
		public IEnumerable<BaseLookupViewItem> Get() {
			return Storage.GetCountries().ToView();
		}
		[HttpGet]
		public CountryView Get(Guid id) {
			return Storage.GetCountry(id).ToView();
		}
		[HttpPost]
		public void Post([FromBody]CountryView item) {
			Storage.AddCountry(item.Cast());
		}
		[HttpPut]
		public void Put([FromBody]CountryView item) {
			Storage.UpdateCountry(item.Cast());
		}
		[HttpDelete]
		public void Delete(Guid id) {
			Storage.DeleteCountry(id);
		}
	}
}