using System;
using System.Collections.Generic;
using System.Linq;

namespace TerritoriesAssignment.Core.Algorithms
{
	public class TerritorySolution<T>
	{
		public Dictionary<T, Territory<T>> Territories { get; }

		public TerritorySolution(Dictionary<T, Territory<T>> startTerritories) {
			Territories = startTerritories;
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

		public virtual int GetBricksCount() {
			return Territories.Select(pair => pair.Value.Bricks.Count).Sum();
		}
	}
}
