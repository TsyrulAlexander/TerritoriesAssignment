using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Core.Db;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.WebApp.Controllers {
	[Route("api/area")]
	public class AreaController : Controller, IStorageController<Area> {
		public IDataStorage Storage { get; }

		public AreaController(IDataStorage storage) {
			Storage = storage;
		}
		[HttpGet("getItems/{countryId?}")]
		public IEnumerable<Area> GetItems([FromQuery]Guid countryId) {
			return Storage.GetAreas(countryId);
		}
		public Area Get(Guid id) {
			return Storage.GetArea(id);
		}
		public void Post(Area item) {
			Storage.AddArea(item);
		}
		public void Put(Area item) {
			Storage.UpdateArea(item);
		}
		public void Delete(Guid id) {
			Storage.DeleteArea(id);
		}
	}
}