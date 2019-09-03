using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Core.Db;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.WebApp.Controllers {
	[Route("api/country")]
	[ApiController]
	public class CountryController : Controller, IStorageController<Country> {
		public IDataStorage Storage { get; }

		public CountryController(IDataStorage storage) {
			Storage = storage;
		}
		[HttpGet("getItems")]
		public IEnumerable<Country> Get() {
			return Storage.GetCountries();
		}
		public Country Get(Guid id) {
			return Storage.GetCountry(id);
		}
		public void Post(Country item) {
			Storage.AddCountry(item);
		}
		public void Put(Country item) {
			Storage.UpdateCountry(item);
		}
		public void Delete(Guid id) {
			Storage.DeleteCountry(id);
		}
	}
}