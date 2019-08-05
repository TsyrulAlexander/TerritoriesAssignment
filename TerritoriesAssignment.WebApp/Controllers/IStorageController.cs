using System;
using Microsoft.AspNetCore.Mvc;

namespace TerritoriesAssignment.WebApp.Controllers {
	interface IStorageController<T> {
		[HttpGet("{id}")]
		T Get(Guid id);
		[HttpPost]
		void Post([FromBody] T item);
		[HttpPut]
		void Put([FromBody] T item);
		[HttpDelete("{id}")]
		void Delete(Guid id);
	}
}
