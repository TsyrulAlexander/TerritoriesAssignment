using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerritoriesAssignment.Core.Algorithms
{
	public abstract class Problem<T>
	{
		protected List<T> _managerIds { get; set; }
		protected List<T> _attributeIds { get; set; }
		public abstract TerritorySolution<T> Solve();
		protected virtual double TargetFunction(TerritorySolution<T> territorySolution) {
			double result = 0;
			foreach (var managerId in _managerIds) {
				foreach (var attributeId in _attributeIds) {
					var averageByManager = GetAttributeAverageByManager(attributeId, territorySolution);
					var attributeSum = GetAttributeSumByAttributeId(managerId, attributeId, territorySolution);
					result += Math.Abs(attributeSum - averageByManager);
				}
			}
			return result;
		}

		protected virtual double GetAttributeAverageByManager(T attributeId, TerritorySolution<T> allCurrentTerritories) {
			var attributeAverageByManager = allCurrentTerritories.Territories.Values
				.Select(territories => territories.GetAttributesSum(attributeId)).Sum();
			attributeAverageByManager /= _managerIds.Count;
			return attributeAverageByManager;
		}

		protected virtual double GetAttributeSumByAttributeId(T managerId, T attributeId, TerritorySolution<T> allCurrentTerritories) {
			return allCurrentTerritories.Territories[managerId].GetAttributesSum(attributeId);
		}

		protected virtual double GetAttributeSumByAttributeId(T attributeId, Territory<T> newTerritory) {
			return newTerritory.GetAttributesSum(attributeId);
		}

		public virtual void PrintTerritorySolution(TerritorySolution<T> territorySolution) {
			Console.WriteLine("Territories: ");
			foreach (var territory in territorySolution.Territories) {
				Console.WriteLine($"	Manager Id = { territory.Key }. Brick Ids: { string.Join(", ", territory.Value)}");
			}
			Console.WriteLine(new string('-', 20));
		}
	}
}
