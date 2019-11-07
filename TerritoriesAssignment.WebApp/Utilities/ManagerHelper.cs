using System;
using System.Collections.Generic;
using System.Linq;
using TerritoriesAssignment.Core;
using TerritoriesAssignment.Core.Algorithms;
using TerritoriesAssignment.Core.Entities.Map;
using TerritoriesAssignment.Core.Utilities;
using TerritoriesAssignment.Database;
using TerritoriesAssignment.WebApp.Models;

namespace TerritoriesAssignment.WebApp.Utilities {
	public class ManagerHelper {
		public IDataStorage Storage { get; }
		public ManagerHelper(IDataStorage storage) {
			Storage = storage;
		}
		public ManagerResponseView[] ManagersDistribution(Guid countryId, ManagerView[] managers) {
			var areas = Storage.GetAreasMap(countryId).ToList();
			var attributes = Storage.GetAttributes().ToList();
			var areaMatrix = GetAreasMatrix(areas);
			var attributeList = attributes.Select(lookup => lookup.Id).ToList();
			var attributeMatrix = GetAttributeMatrix(areas, attributes);
			var managersList = managers.Select(manager => manager.Id).ToList();
			var startAreasList = managers.Select(manager => manager.Area.Id).ToList();
			var algorithm = new SequentialHeuristicAlgorithm<Guid>(managersList, attributeList, areaMatrix, attributeMatrix, startAreasList);
			var territorySolutionResponse = algorithm.Solve();
			var algorithm2 = new LocalSearchAlgorithm<Guid>(territorySolutionResponse, managersList, attributeList, areaMatrix, attributeMatrix);
			var territorySolutionResponse2 = algorithm2.Solve();
			return territorySolutionResponse2.Territories.Select(pair => new ManagerResponseView {
				Id = pair.Key,
				Areas = pair.Value.Bricks.Select(brick => {
					var mapArea = areas.First(lookup => lookup.Id == brick.Id);
					return new BaseMapViewItem(mapArea);
				}).ToArray()
			}).ToArray();
		}

		private Dictionary<Guid, List<Guid>> GetAreasMatrix(List<BaseMapLookup> areas) {
			var collection = new Dictionary<Guid, List<Guid>>();
			foreach (var area in areas) {
				var mapDistances = MapUtilities.GetMapDistances(area, areas).ToArray();
				var minDistance = mapDistances.Min(distance => distance.MinDistance);
				var nearbyAreas = mapDistances.Where(distance => Math.Abs(distance.MinDistance - minDistance) < 0.0001);
				collection.Add(area.Id, nearbyAreas.Select(distance => distance.MapLookup.Id).ToList());
			}
			return collection;
		}

		private Dictionary<Guid, Dictionary<Guid, double>> GetAttributeMatrix(List<BaseMapLookup> areas, List<BaseLookup> attributes) {
			var collection = new Dictionary<Guid, Dictionary<Guid, double>>();
			foreach (var area in areas) {
				var attributeValues = Storage.GetAttributeValuesFromArea(area.Id).ToArray();
				var attributeCollection = new Dictionary<Guid, double>(attributes.Count);
				foreach (var attribute in attributes) {
					var attributeValue = attributeValues.FirstOrDefault(value => value.Attribute.Id == attribute.Id);
					var attributeDoubleValue = attributeValue?.DoubleValue ?? 0;
					attributeCollection.Add(attribute.Id, attributeDoubleValue);
				}
				collection.Add(area.Id, attributeCollection);
			}
			return collection;
		}
	}
}