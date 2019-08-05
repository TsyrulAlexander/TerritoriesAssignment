using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Core.Db;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.WebApp.Controllers {
	[Route("api/region")]
	public class RegionController : Controller, IStorageController<Region> {
		public IDataStorage Storage { get; }

		public RegionController(IDataStorage storage) {
			Storage = storage;
		}
		[HttpGet("getItems/{areaId?}")]
		public IEnumerable<Region> GetItems(Guid areaId) {
			return Storage.GetRegions(areaId);
		}
		public Region Get(Guid id) {
			return Storage.GetRegion(id);
		}
		public void Post(Region item) {
			Storage.AddRegion(item);
		}
		public void Put(Region item) {
			Storage.UpdateRegion(item);
		}
		public void Delete(Guid id) {
			Storage.DeleteRegion(id);
		}
	}
}
