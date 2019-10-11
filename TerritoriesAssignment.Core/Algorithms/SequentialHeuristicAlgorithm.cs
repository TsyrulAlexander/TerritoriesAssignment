using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace TerritoriesAssignment.Core.Algorithms
{
	class SequentialHeuristicAlgorithm
	{
		private Dictionary<int, List<int>> NeighborhoodMatrix; //n * n
		private IEnumerable<Brick<int>> AllBricks;
		private int[,] AttributiveMatrix; // n * k
		private int ManagerCount;
		private int BricksCount;
		private int AttributesCount;
		private List<Brick<int>> StartBricks { get; set; }

		public SequentialHeuristicAlgorithm() {
			ManagerCount = 4;
			StartBricks = new List<Brick<int>> {
				new Brick<int>(7),
				new Brick<int>(5),
				new Brick<int>(4),
				new Brick<int>(16)
			};
			NeighborhoodMatrix = new Dictionary<int, List<int>> {
				{ 0, new List<int> { 4, 12, 17 }},
				{ 1, new List<int> { 7, 6, 2 }},
				{ 2, new List<int> { 1, 5, 3 }},
				{ 3, new List<int> { 2, 5, 11, 4 }},
				{ 4, new List<int> { 3, 11, 12, 0 }},
				{ 5, new List<int> { 1, 2, 3, 11, 10, 6 }},
				{ 6, new List<int> { 1, 2, 5, 10, 7 }},
				{ 7, new List<int> { 1, 6, 10, 9, 8 }},
				{ 8, new List<int> { 7, 9 }},
				{ 9, new List<int> { 8, 7, 10, 14 }},
				{ 10, new List<int> { 6, 5, 11, 13, 14, 9, 7 }},
				{ 11, new List<int> { 5, 3, 4, 12, 13, 10 }},
				{ 12, new List<int> { 4, 0, 17, 16, 13, 11 }},
				{ 13, new List<int> { 10, 11, 12, 16, 15, 14 }},
				{ 14, new List<int> { 9, 10, 13, 15 }},
				{ 15, new List<int> { 14, 13, 16 }},
				{ 16, new List<int> { 15, 13, 12, 17 }},
				{ 17, new List<int> { 16, 12 }}
			};
			AttributiveMatrix = new[,]
			{
				{ 5 , 18 },
				{ 9 , 8 },
				{ 14, 7 },
				{ 16, 6 },
				{ 1 , 6 },
				{ 12, 4 },
				{ 15, 18 },
				{ 9 , 1 },
				{ 9 , 14 },
				{ 7 , 20 },
				{ 13, 11 },
				{ 3 , 20 },
				{ 7 , 14 },
				{ 7 , 11 },
				{ 17, 7  },
				{ 12, 21 },
				{ 8 , 1  },
				{ 19, 6 }
			};
			AllBricks = NeighborhoodMatrix.Select(pair => new Brick<int>(pair.Key, pair.Value.Select(id => new Brick<int>(id))));
			BricksCount = NeighborhoodMatrix.Count;
			for (int i = 0; i < AttributiveMatrix.Length/2; i++) {
				var attributes = new Dictionary<int, Attribute<double>>();
				for (int j = 0; j < 2; j++) {
					attributes.Add(j, new Attribute<double>(AttributiveMatrix[i, j]));
				}
				AllBricks.FirstOrDefault(x => x.Id.Equals(i)).Attributes = new DoubleBrickAttributes<int>(attributes);
			}
		}

		public TerritorySolution<int> Solve() {	 // m * n
			var	resultTerritories = InitStartBricks();
			while (resultTerritories.GetBricksCount() != BricksCount) {  //while all bricks will be assigned
				foreach (var currentTerritory in resultTerritories.Territories.Values) {
					var validBricksToJoin = GetValidBricksToJoin(currentTerritory, resultTerritories);
					foreach (var validBricks in validBricksToJoin) {
						var tempTerritory = (Territory<int>) currentTerritory.Clone();
						tempTerritory.AddBrick(validBricks);
					}
				}
			}
			return resultTerritories;
		}

		/// <summary>
		/// Init start territories which consist of one brick per each.
		/// </summary>
		/// <returns></returns>
		private TerritorySolution<int> InitStartBricks() {
			var currentTerritories = new Dictionary<int, Territory<int>>();
			for (int i = 0; i < ManagerCount; i++) {
				var startBrickId = StartBricks.ToArray()[i].Id;
				currentTerritories.Add(i, new Territory<int>(i, new Brick<int>(startBrickId)));
			}
			return new TerritorySolution<int>(currentTerritories);
		}

		private IEnumerable<Brick<int>> GetValidBricksToJoin(Territory<int> currentTerritory,
			TerritorySolution<int> allCurrentTerritories) {
			var validBricks = new HashSet<Brick<int>>();
			foreach (var brick in currentTerritory.Bricks) {
				validBricks.UnionWith(brick.NeighborhoodBricks);
			}
			validBricks.ExceptWith(currentTerritory.Bricks);
			validBricks.ExceptWith(allCurrentTerritories.GetAllBricks());
			return validBricks.ToList();
		}

		private double TargetFunction(Territory<int> newTerritory, TerritorySolution<int> allCurrentTerritories) {
			double result = 0;
			for (int i = 0; i < AttributesCount; i++) {
				var averageByManager = GetAttribyteAverageByManager(i, allCurrentTerritories);
			}
			 
			return result;
		}

		private double GetAttribyteAverageByManager(int attributeId, TerritorySolution<int> allCurrentTerritories) {
			var attribyteAverageByManager = allCurrentTerritories.Territories.Values
				.Select(territories => territories.GetAttributesSum(attributeId)).Sum();
			attribyteAverageByManager /= ManagerCount;
			return attribyteAverageByManager;
		} 
	}
}
