using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Core.Db;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.WebApp.Controllers {
	[Route("api/country")]
	public class CountryController : Controller {
		public IDataStorage Storage { get; }

		public CountryController(IDataStorage storage) {
			Storage = storage;
		}
		[HttpGet]
		public IEnumerable<Country> GetCountries() {
			return Storage.GetCountries();
		}
	}
}