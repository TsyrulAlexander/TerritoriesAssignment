using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Core;
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
		[HttpPost("add")]
		public void Post([FromBody]AreaView item) {
			Storage.AddArea(item.Cast());
		}
		[HttpPost("update")]
		public void Put([FromBody]AreaView item) {
			Storage.UpdateArea(item.Cast());
		}
		[HttpDelete("{id}")]
		public void Delete(Guid id) {
			Storage.DeleteArea(id);
		}
	}
}