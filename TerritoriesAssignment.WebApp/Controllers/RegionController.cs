using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Database;
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
		public RegionView Get(Guid id) {
			return Storage.GetRegion(id).ToView();
		}
		public void Post(RegionView item) {
			Storage.AddRegion(item.Cast());
		}
		public void Put(RegionView item) {
			Storage.UpdateRegion(item.Cast());
		}
		public void Delete(Guid id) {
			Storage.DeleteRegion(id);
		}
	}
}
