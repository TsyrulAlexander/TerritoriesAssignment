using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Database;

namespace TerritoriesAssignment.WebApp.Controllers {
	[Route("api/home")]
	public class HomeController : Controller {
		public IDataStorage Storage { get; }

		public HomeController(IDataStorage storage) {
			Storage = storage;
		}
		[HttpPost]
		public string Post([FromBody]string value) {
			return $"Text: {value} val";
		}
	}
}
