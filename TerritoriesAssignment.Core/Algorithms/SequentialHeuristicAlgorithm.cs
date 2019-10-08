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
		private int[,] AttributiveMatrix; // n * k
		private int ManagerCount;
		private int[] StartBricks1 { get; set; }
		private List<Brick<int>> StartBricks { get; set; }

		public SequentialHeuristicAlgorithm() {
			ManagerCount = 4;
			StartBricks1 = new[] { 7, 5, 4, 16 };
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
				{ 3, new List<int> { 2, 5, 11, 4 }}, //3
				{ 4, new List<int> { 3, 11, 12, 0 }},
				{ 5, new List<int> { 1, 2, 3, 11, 10, 6 }},  //5
				{ 6, new List<int> { 1, 2, 5, 10, 7 }},
				{ 7, new List<int> { 1, 6, 10, 9, 8 }},	//7
				{ 8, new List<int> { 7, 9 }},
				{ 9, new List<int> { 8, 7, 10, 14 }},	//9
				{ 10, new List<int> { 6, 5, 11, 13, 14, 9, 7 }},
				{ 11, new List<int> { 5, 3, 4, 12, 13, 10 }}, //11
				{ 12, new List<int> { 4, 0, 17, 16, 13, 11 }},
				{ 13, new List<int> { 10, 11, 12, 16, 15, 14 }},	//13
				{ 14, new List<int> { 9, 10, 13, 15 }},
				{ 15, new List<int> { 14, 13, 16 }},//15
				{ 16, new List<int> { 15, 13, 12, 17 }},
				{ 17, new List<int> { 16, 12 }}//17
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
		}

		public Dictionary<int, List<int>> Solve() {	 // m * n
			var	resultTerritories = InitStartBricks();
			for (int i = 0; i < ManagerCount; i++) {
				
			}
			return resultTerritories;
		}

		private TerritorySolution<int> InitStartBricks() {
			//var currentTerritories1 = new Dictionary<int, List<int>>();
			var currentTerritories = new List<Territory<int>>();
			for (int i = 0; i < ManagerCount; i++) {
				//currentTerritories1.Add(i, new List<int> { StartBricks1[i] });
				currentTerritories.Add();
			}
			return new TerritorySolution<int>(currentTerritories);
		}

		private IEnumerable<int> GetValidBricksToJoin(int[] currentTerritory,
			Dictionary<int, List<int>> allCurrentTerritories) {
			var validBricks = new HashSet<int>();
			for (int i = 0; i < currentTerritory.Length; i++) {
				foreach (var neighborhoodId in NeighborhoodMatrix[i]) {
						validBricks.Add(neighborhoodId);
				}
			}
			validBricks.ExceptWith(currentTerritory);
			for (int i = 0; i < ManagerCount; i++) {
				validBricks.ExceptWith(allCurrentTerritories[i]);
			}
			//validBricks


			return validBricks.ToList();
		}
	}

	public class TerritorySolution<T>
	{
		protected List<Territory<T>> Territories { get; }

		public TerritorySolution(List<Territory<T>> startTerritories) {
			Territories = startTerritories;
		}
	}

	public class Territory<T>
	{
		protected T Id { get; }
		protected List<Brick<T>> Bricks { get; } = new List<Brick<T>>();

		public Territory(T territoryId, Brick<T> firstBrick) {
			Id = territoryId;
			Bricks.Add(firstBrick);
		}

		public Territory(T territoryId, List<Brick<T>> bricks) {
			Id = territoryId;
			Bricks = bricks;
		}
	}

	public class Brick<T>
	{
		protected T Id { get; }

		public Brick(T brickId) {
			Id = brickId;
		}
	}
}
