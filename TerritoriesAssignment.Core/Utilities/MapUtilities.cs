using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using TerritoriesAssignment.Core.Entities.Map;

namespace TerritoriesAssignment.Core.Utilities {
	public static class MapUtilities {
		public static MapIntersectionType GetIntersectionType(string firstPath, string secondPath) {
			var firstGeometry = Geometry.Parse(firstPath);
			var secondGeometry = Geometry.Parse(secondPath);
			var firstPathGeometry = PathGeometry.CreateFromGeometry(firstGeometry);
			var secondPathGeometry = PathGeometry.CreateFromGeometry(secondGeometry);
			return (MapIntersectionType) firstPathGeometry.FillContainsWithDetail(secondPathGeometry);
		}

		public static IEnumerable<BaseMapLookup> GetIntersection(BaseMapLookup map, IEnumerable<BaseMapLookup> maps) {
			var successIntersectionTypes = new [] {
				MapIntersectionType.FullyContains,
				MapIntersectionType.FullyInside,
				MapIntersectionType.Intersects
			};
			return maps.Where(lookup =>
				lookup != map && successIntersectionTypes.Contains(GetIntersectionType(map.Path, lookup.Path)));
		}
	}
}