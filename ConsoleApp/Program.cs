using System;
using System.Collections.Generic;
using System.Linq;
using SQLiteFramework;
using SQLiteFramework.Query;
using TerritoriesAssignment.Core;
using TerritoriesAssignment.Core.Algorithms;
using TerritoriesAssignment.Core.Entities;
using TerritoriesAssignment.Core.Entities.Map;
using TerritoriesAssignment.Core.Utilities;
using TerritoriesAssignment.Database.Storages.Mock;
using TerritoriesAssignment.Database.Storages.SQLite;
using TerritoriesAssignment.WebApp.Models;
using TerritoriesAssignment.WebApp.Utilities;

namespace ConsoleApp {
	class Program {
		private double FirstAlgTotalTime;
		private double SecondAlgTotalTime;
		static void Main(string[] args) {
			var path = @"C:\Temp\azaza.db";
			SQLiteDatabaseCreator creator = new SQLiteDatabaseCreator(path);
			creator.CreateSettingTable();
			creator.CreateSettingValueTable();
			var storage = new SQLiteDataStorage(path);
			var areas = storage.GetAreas(Guid.Parse("74764ac0-75fb-41c6-af39-a0ffffa83bcb")).Where(x => x.Name.Equals("Rivne"));
			var startAreasCollection = new[] {new[] { "Lviv", "Dnipropetrovsk", "Chernihiv"/*, "Zhytomyr", "Crimea", "Kirovohrad"*/
		}, 
				new[] { "Volyn", "Kherson", "Mykolayiv"/*, "Luhansk","Chernihiv", "Kiev" */},
				new[] { "Odessa", "Sumy", "Transcarpathia"/*, "Volyn", "Cherkasy", "Kiev"*/ },
				new[] { "Vinnytsya", "Poltava", "Zaporizhzhya"/*, "Ivano-Frankivsk", "Crimea", "Kirovohrad" */ }
			};
			long FirstAlgTotalTime= 0;
			long SecondAlgTotalTime=0;
			double FirstAlgAvarageTargetFunc = 0;
			double SecondAlgAvarageTargetFunc = 0;
			foreach (var startAreas in startAreasCollection) {
				var managers = GetManagerViews(storage, startAreas);
				var territorySolution = GetTerritorySolution(storage, Guid.Parse("74764ac0-75fb-41c6-af39-a0ffffa83bcb"), managers, out var targetFunc1);
				FirstAlgTotalTime += territorySolution.ElapsedTime;
				FirstAlgAvarageTargetFunc += targetFunc1;
				var territorySolution2 = GetTerritorySolution(storage, Guid.Parse("74764ac0-75fb-41c6-af39-a0ffffa83bcb"), managers, out var targetFunc2, territorySolution);
				SecondAlgTotalTime += territorySolution2.ElapsedTime;
				SecondAlgAvarageTargetFunc += targetFunc2;
			}
			var firstAcarage = FirstAlgTotalTime / startAreasCollection.Length;
			var secondAcarage = SecondAlgTotalTime / startAreasCollection.Length;
			FirstAlgAvarageTargetFunc /= startAreasCollection.Length;
			SecondAlgAvarageTargetFunc /= startAreasCollection.Length;
		}


		public static TerritorySolution<Guid> GetTerritorySolution(SQLiteDataStorage storage, Guid countryId, ManagerView[] managers, out double targetFunc1, TerritorySolution<Guid> territorySolution = null) {
			var areas = storage.GetAreasMap(countryId).ToList();
			var attributes = storage.GetAttributes().ToList();
			var areaMatrix = GetAreasMatrix(areas);
			var attributeList = attributes.Select(lookup => lookup.Id).ToList();
			var attributeMatrix = GetAttributeMatrix(storage, areas, attributes);
			var managersList = managers.Select(manager => manager.Id).ToList();
			var startAreasList = managers.Select(manager => manager.Area.Id).ToList();
			if (territorySolution == null) {
				var algorithm = new SequentialHeuristicAlgorithm<Guid>(managersList, attributeList, areaMatrix, attributeMatrix, startAreasList);
				var solution = algorithm.Solve();
				targetFunc1 = algorithm.TargetFunction();
				return solution;
			} else {
				var algorithm2 = new LocalSearchAlgorithm<Guid>(territorySolution, managersList, attributeList, areaMatrix, attributeMatrix);
				var solution = algorithm2.Solve();
				targetFunc1 = algorithm2.TargetFunction();
				return solution;
			}
		}

		private static Dictionary<Guid, List<Guid>> GetAreasMatrix(List<BaseMapLookup> areas) {
			var collection = new Dictionary<Guid, List<Guid>>();
			foreach (var area in areas) {
				var mapDistances = MapUtilities.GetMapDistances(area, areas).ToArray();
				var minDistance = mapDistances.Min(distance => distance.MinDistance);
				var nearbyAreas = mapDistances.Where(distance => Math.Abs(distance.MinDistance - minDistance) < 0.0001);
				collection.Add(area.Id, nearbyAreas.Select(distance => distance.MapLookup.Id).ToList());
			}
			return collection;
		}
		private static Dictionary<Guid, Dictionary<Guid, double>> GetAttributeMatrix(SQLiteDataStorage storage, List<BaseMapLookup> areas, List<BaseLookup> attributes) {
			var collection = new Dictionary<Guid, Dictionary<Guid, double>>();
			foreach (var area in areas) {
				var attributeValues = storage.GetAttributeValuesFromArea(area.Id).ToArray();
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

		private static ManagerView[] GetManagerViews(SQLiteDataStorage storage, string[] areaNames) {
			return areaNames.Select(name => new ManagerView {
				Id = Guid.NewGuid(),
				Area = new BaseLookupViewItem {
					Id = storage.GetAreas(Guid.Parse("74764ac0-75fb-41c6-af39-a0ffffa83bcb")).First(x => x.Name.Equals(name)).Id
				}
			}).ToArray();
		}
	}
}
