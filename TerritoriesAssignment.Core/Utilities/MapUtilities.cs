using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TerritoriesAssignment.Core.Entities.Map;

namespace TerritoriesAssignment.Core.Utilities {
	public static class MapUtilities {
		private class MapPoint {
			public double X { get; set; }
			public double Y { get; set; }
		}

		public static IEnumerable<MapPointDistance> GetMapDistances(BaseMapLookup map, IEnumerable<BaseMapLookup> maps) {
			var mapPoints = GetPoints(map.Path);
			return maps.Select(mapItem => {
				var mapItemPoints = GetPoints(mapItem.Path);
				var mapPointDistance = GetMinDistance(mapPoints, mapItemPoints);
				return new MapPointDistance {
					MapLookup = mapItem,
					MinDistance = mapPointDistance
				};
			});
		}

		private static double GetMinDistance(IEnumerable<MapPoint> firstPoints, IEnumerable<MapPoint> secondPoints) {
			return firstPoints.SelectMany(firstPoint => secondPoints, GetDistance).Min();
		}

		private static IEnumerable<MapPoint> GetPoints(string pathStr) {
			var points = new List<MapPoint>();
			var lastStartPointIndex = 0;
			for (var i = 0; i < pathStr.Length;) {
				if (pathStr[i] == 'M') {
					lastStartPointIndex = points.Count;
					points.Add(GetPointFromStr(pathStr, ref i));
				} else if (pathStr[i] == 'm') {
					lastStartPointIndex = points.Count;
					points.Add(GetPointRelativeFromStr(points[points.Count - 1], pathStr, ref i));
				} else if (pathStr[i] == 'L') {
					points.Add(GetPointFromStr(pathStr, ref i));
				} else if (pathStr[i] == 'l') {
					points.Add(GetPointRelativeFromStr(points[points.Count - 1], pathStr, ref i));
				} else if(pathStr[i] == 'H') {
					points.Add(new MapPoint {
						X = GetPointValue(pathStr, ref i),
						Y = points[points.Count - 1].Y
					});
				} else if (pathStr[i] == 'h') {
					points.Add(new MapPoint {
						X = GetPointXRelativeValue(points[points.Count - 1], pathStr, ref i),
						Y = points[points.Count - 1].Y
					});
				} else if (pathStr[i] == 'V') {
					points.Add(new MapPoint {
						X = points[points.Count - 1].X,
						Y = GetPointValue(pathStr, ref i)
					});
				} else if (pathStr[i] == 'v') {
					points.Add(new MapPoint {
						X = points[points.Count - 1].X,
						Y = GetPointYRelativeValue(points[points.Count - 1], pathStr, ref i)
					});
				} else if (pathStr[i] == 'Z' || pathStr[i] == 'z') {
					points.Add(points[lastStartPointIndex]);
					i++;
				} else {
					i++;
				}
			}
			return points;
		}

		private static double GetPointXRelativeValue(MapPoint parent, string str, ref int index) {
			return GetFirstDoubleValue(str, ref index) + parent.X;
		}

		private static double GetPointYRelativeValue(MapPoint parent, string str, ref int index) {
			return GetFirstDoubleValue(str, ref index) + parent.Y;
		}

		private static double GetPointValue(string str, ref int index, double shift = 0) {
			return GetFirstDoubleValue(str, ref index) + shift;
		}

		private static double GetFirstDoubleValue(string str, ref int index) {
			var startIndex = ++index;
			var currentChar = str[index];
			while (true) {
				if (!char.IsNumber(currentChar) && currentChar != '.' && currentChar != '-') {
					break;
				}
				currentChar = str[++index];
			}
			return double.Parse(str.Substring(startIndex, index - startIndex), CultureInfo.InvariantCulture);
		}

		private static MapPoint GetPointRelativeFromStr(MapPoint parent, string str, ref int index) {
			return new MapPoint {
				X = GetPointXRelativeValue(parent, str, ref index),
				Y = GetPointYRelativeValue(parent, str, ref index)
			};
		}

		private static MapPoint GetPointFromStr(string str, ref int index) {
			return new MapPoint {
				X = GetPointValue(str, ref index),
				Y = GetPointValue(str, ref index)
			};
		}

		private static double GetDistance(MapPoint point1, MapPoint point2) {
			var a = point2.X - point1.X;
			var b = point2.Y - point1.Y;
			return Math.Sqrt(a * a + b * b);
		}

	}
	
}