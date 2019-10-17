using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Database;
using TerritoriesAssignment.WebApp.Models;
using TerritoriesAssignment.WebApp.Utilities;

namespace TerritoriesAssignment.WebApp.Controllers {
	[Route("api/area")]
	public class AreaController : Controller, IStorageController<AreaView> {
		public IDataStorage Storage { get; }

		public AreaController(IDataStorage storage) {
			Storage = storage;
		}
		[HttpGet("getItems/{countryId?}")]
		public IEnumerable<BaseLookupViewItem> GetItems([FromQuery]Guid countryId) {
			return Storage.GetAreas(countryId).ToView();
		}
		[HttpGet]
		public AreaView Get(Guid id) {
			return Storage.GetArea(id).ToView();
		}
		[HttpPost]
		public void Post([FromBody]AreaView item) {
			Storage.AddArea(item.Cast());
		}
		[HttpPut]
		public void Put([FromBody]AreaView item) {
			Storage.UpdateArea(item.Cast());
		}
		[HttpDelete]
		public void Delete(Guid id) {
			Storage.DeleteArea(id);
		}
	}
}