using System;
using System.Collections.Generic;
using System.Linq;

namespace TerritoriesAssignment.Core.Entities.Map {
	public class BaseMapLookup : BaseLookup {
		public string MapPoint { get; set; }
		public IEnumerable<MapPoint> GetPoints() {
			return ParseCoordinateList(MapPoint);
		}

		public IEnumerable<MapPoint> ParseCoordinateList(string coordinateStr) {
			return MapPoint.Split(new []{ ',' }, StringSplitOptions.RemoveEmptyEntries).Select(ParseCoordinate);
		}

		public MapPoint ParseCoordinate(string coordinateStr) {
			var coordinate = coordinateStr.Split(':');
			if (coordinate.Length != 2) {
				throw new FormatException();
			}
			var x = float.Parse(coordinate[0]);
			var y = float.Parse(coordinate[1]);
			return new MapPoint {
				X = x,
				Y = y
			};
		}
	}
}