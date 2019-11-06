using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Database;
using TerritoriesAssignment.WebApp.Models;

namespace TerritoriesAssignment.WebApp.Controllers {
	[Route("api/map")]
	[ApiController]
	public class MapController : Controller {
		public IDataStorage Storage { get; }
		public MapController(IDataStorage storage) {
			Storage = storage;
		}
		[HttpGet("getAreas")]
		public BaseMapViewItem[] GetAreasMap(Guid countryId) {
			return Storage.GetAreasMap(countryId).Select(map => new BaseMapViewItem(map)).ToArray();
		}
		[HttpGet("getArea")]
		public BaseMapViewItem GetAreaMap(Guid areaId) {
			return new BaseMapViewItem(Storage.GetArea(areaId));
		}
		[HttpPost("getAreas")]
		public BaseMapViewItem[] GetAreasMap([FromBody] Guid[] areas) {
			return areas.Select(areaId => new BaseMapViewItem(Storage.GetArea(areaId))).ToArray();
		}
	}
}