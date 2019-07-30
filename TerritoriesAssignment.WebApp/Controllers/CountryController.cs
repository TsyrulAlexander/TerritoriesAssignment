using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Core.Db;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.WebApp.Controllers {
	[Route("api/country")]
	public class CountryController : Controller, IStorageController<Country> {
		public IDataStorage Storage { get; }

		public CountryController(IDataStorage storage) {
			Storage = storage;
		}
		[HttpGet]
		public IEnumerable<Country> Get() {
			return Storage.GetCountries();
		}
		public Country Get(Guid id) {
			throw new NotImplementedException();
		}
		public void Post(Country item) {
			throw new NotImplementedException();
		}
		public void Put(int id, Country item) {
			throw new NotImplementedException();
		}
		public void Delete(Guid id) {
			throw new NotImplementedException();
		}
	}
}