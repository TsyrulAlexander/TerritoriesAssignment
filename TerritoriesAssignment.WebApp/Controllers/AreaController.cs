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
		public AreaView Get(Guid id) {
			return Storage.GetArea(id).ToView();
		}
		public void Post(AreaView item) {
			Storage.AddArea(item.Cast());
		}
		public void Put(AreaView item) {
			Storage.UpdateArea(item.Cast());
		}
		public void Delete(Guid id) {
			Storage.DeleteArea(id);
		}
	}
}