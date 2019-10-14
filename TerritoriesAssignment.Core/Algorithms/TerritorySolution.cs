using System;
using System.Collections.Generic;
using System.Linq;
using TerritoriesAssignment.Core.Utilities;

namespace TerritoriesAssignment.Core.Algorithms
{
	[Serializable]
	public class TerritorySolution<T> : ICloneable
	{
		public Dictionary<T, Territory<T>> Territories { get; }

		public TerritorySolution(Dictionary<T, Territory<T>> startTerritories) {
			Territories = startTerritories;
		}

		public virtual TerritorySolution<T> AddBrick(T territoryId, Brick<T> brick) {
			if (brick == null) {
				throw new ArgumentException($"TerritoryId = {territoryId}. Unknown brick.");
			}
			Territories[territoryId].Bricks.Add(brick);
			return this;
		}

		public virtual IEnumerable<Brick<T>> GetAllBricks() {
			var bricks = new List<Brick<T>>();
			foreach (var territory in Territories.Values) {
				bricks.AddRange(territory.Bricks);
			}
			return bricks;
		}

		public virtual IEnumerable<T> GetAllBrickIds() {
			var ids = new List<T>();
			foreach (var territory in Territories) {
				ids.AddRange(territory.Value.GetBrickIds());
			}
			return ids;
		}

		public double GetAttributesSum() {
			return Territories.Values.Select(territory => territory.GetAttributesSum()).Sum();
		}

		public double GetAttributesSum(T attributeId) {
			return Territories.Values.Select(territory => territory.GetAttributesSum(attributeId)).Sum();
		}

		public virtual int GetBricksCount() {
			return Territories.Select(pair => pair.Value.Bricks.Count).Sum();
		}

		public object Clone() {
			return this.DeepClone();
		}
	}
}
