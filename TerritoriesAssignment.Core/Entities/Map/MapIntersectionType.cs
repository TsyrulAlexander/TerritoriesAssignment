using System;
using System.Collections.Generic;
using System.Text;

namespace TerritoriesAssignment.Core.Entities.Map {
	public enum MapIntersectionType {
		Empty = 1,
		FullyContains = 2,
		FullyInside = 3,
		Intersects = 4,
		NotCalculated = 5
	}
}