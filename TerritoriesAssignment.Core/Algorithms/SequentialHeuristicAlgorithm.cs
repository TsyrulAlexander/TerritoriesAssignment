using System;
using System.Collections.Generic;
using System.Text;

namespace TerritoriesAssignment.Core.Algorithms
{
	class SequentialHeuristicAlgorithm
	{
		private IEnumerable<IEnumerable<int>> NeighborhoodMatrix; //n * n
		private int[,] AttributiveMatrix; // n * k
		private int ManagerCount;
		private int[] StartBricks { get; set; }

		public SequentialHeuristicAlgorithm() {
			ManagerCount = 4;
			StartBricks = new[] { 7, 5, 4, 16 };
			NeighborhoodMatrix = new[] {	   //!!!!!!!!!!!!!!!!!!!!!!!!
				new[] { 4, 12, 17 },
				new[] { 7, 6, 2 },
				new[] { 1, 5, 3 },
				new[] { 2, 5, 11, 4 }, //3
				new[] { 3, 11, 12, 0 },
				new[] { 1, 2, 3, 11, 10, 6 },  //5
				new[] { 1, 2, 5, 10, 7 },
				new[] { 1, 6, 10, 9, 8 },	//7
				new[] { 7, 9 },
				new[] { 8, 7, 10, 14 },	//9
				new[] { 6, 5, 11, 13, 14, 9, 7 },
				new[] { 5, 3, 4, 12, 13, 10 }, //11
				new[] { 4, 0, 17, 16, 13, 11 },
				new[] { 10, 11, 12, 16, 15, 14 },	//13
				new[] { 9, 10, 13, 15 },
				new[] { 14, 13, 16 },//15
				new[] { 15, 13, 12, 17 },
				new[] { 16, 12 }, //17
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

		public IEnumerable<IEnumerable<int>> Solve() {	 // m * n
			var	resultTerritories = InitStartBricks();
			for (int i = 0; i < ManagerCount; i++) {
				
			}
			return resultTerritories;
		}

		private IEnumerable<IEnumerable<int>> InitStartBricks() {
			var currentTerritories = new List<IEnumerable<int>>(ManagerCount);
			for (int i = 0; i < ManagerCount; i++) {
				currentTerritories.Add(new List<int> { StartBricks[i] });
			}
			return currentTerritories;
		}

		private IEnumerable<int> GetValidBricksToJoin(List<int> currentTerritory,
			List<List<int>> allCurrentTerritories) {
			var validBricks = new List<int>();
			for (int i = 0; i < ManagerCount; i++) {
				//NeighborhoodMatrix[1, 2] = 3;
			}
			return validBricks;
		}
	}
}
