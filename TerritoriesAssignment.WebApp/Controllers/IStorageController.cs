using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.WebApp.Controllers
{
	interface IStorageController<T> {
		[HttpGet("{id}")]
		T Get(Guid id);
		[HttpPost]
		void Post([FromBody] T item);
		[HttpPut("{id}")]
		void Put(int id, [FromBody] T item);
		[HttpDelete("{id}")]
		void Delete(Guid id);
	}
}
