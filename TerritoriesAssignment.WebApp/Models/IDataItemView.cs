using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TerritoriesAssignment.WebApp.Models
{
	interface IDataItemView<T> {
		T Cast();
	}
}
