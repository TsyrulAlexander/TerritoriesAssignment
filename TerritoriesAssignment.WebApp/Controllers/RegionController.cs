using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Core;
using TerritoriesAssignment.WebApp.Models;
using TerritoriesAssignment.WebApp.Utilities;

namespace TerritoriesAssignment.WebApp.Controllers {
	[Route("api/region")]
	public class RegionController : Controller, IStorageController<RegionView> {
		public IDataStorage Storage { get; }

		public RegionController(IDataStorage storage) {
			Storage = storage;
		}
		[HttpGet("getItems/{areaId?}")]
		public IEnumerable<BaseLookupViewItem> GetItems(Guid areaId) {
			return Storage.GetRegions(areaId).ToView();
		}
		[HttpGet]
		public RegionView Get(Guid id) {
			return Storage.GetRegion(id).ToView();
		}
		[HttpPost("add")]
		public void Post([FromBody]RegionView item) {
			Storage.AddRegion(item.Cast());
		}
		[HttpPost("update")]
		public void Put([FromBody]RegionView item) {
			Storage.UpdateRegion(item.Cast());
		}
		[HttpDelete("{id}")]
		public void Delete(Guid id) {
			Storage.DeleteRegion(id);
		}
	}
}
