using System.Collections.Generic;
using TerritoriesAssignment.Core.Entities.Map;

namespace TerritoriesAssignment.Core.Entities {
	public class Country : BaseMapLookup {
		public List<Area> Areas { get; set; }
	}
}
