using System;
using System.Collections.Generic;
using System.Linq;

namespace TerritoriesAssignment.Core.Db.Map
{
	public class BaseMapLookup: BaseLookup
	{
		public string Path { get; set; }
		public IEnumerable<MapPoint> GetPoints() {
			return Path.Split(",", StringSplitOptions.RemoveEmptyEntries)
			.Select(coordinateStr => {
					var coordinate = coordinateStr.Split(":");
					var x = float.Parse(coordinate[0]);
					var y = float.Parse(coordinate[1]);
					return new MapPoint {
						X = x,
						Y = y
					};
			});
		}
	}
}
