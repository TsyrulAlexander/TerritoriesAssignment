using System.Collections.Generic;
using TerritoriesAssignment.Core.Entities.Map;

namespace TerritoriesAssignment.Core.Entities {
	public class Area : BaseMapLookup {
		public BaseLookup Country { get; set; }
		public List<Region> Regions { get; set; }
	}
}