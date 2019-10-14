using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TerritoriesAssignment.Core.Utilities;

namespace TerritoriesAssignment.Core.Algorithms
{
	public class SequentialHeuristicAlgorithm
	{
		private readonly int _managerCount;
		private readonly int _bricksCount;
		private readonly int _attributesCount;
		private readonly IEnumerable<Brick<int>> _allBricks;
		private readonly List<Brick<int>> _startBricks;
		private readonly Dictionary<int, List<int>> _neighborhoodMatrix; //n * n
		private readonly Dictionary<int, List<int>> _attributiveMatrix; // n * k

		public SequentialHeuristicAlgorithm(int managerCount, Dictionary<int, List<int>> neighborhoodMatrix, 
			Dictionary<int, List<int>> attributiveMatrix, int attributesCount, IEnumerable<int> startBrickIds) {
			
			_managerCount = managerCount;
			_bricksCount = neighborhoodMatrix.Count;
			_attributesCount = attributesCount;
			_allBricks = GetAllBricks(neighborhoodMatrix, attributiveMatrix);
			_startBricks = _allBricks.Where(brick => startBrickIds.Contains(brick.Id)).ToList();
			_neighborhoodMatrix = neighborhoodMatrix;
			_attributiveMatrix = attributiveMatrix;
		}

		public TerritorySolution<int> Solve() {	 // m * n
			var	resultTerritories = InitStartBricks();
			PrintTerritorySolution(resultTerritories);
			while (resultTerritories.GetBricksCount() != _bricksCount) //while all bricks will be assigned
			{
				foreach (var currentTerritoryKey in resultTerritories.Territories.Keys) // for each territory
				{
					var currentTerritoryValue = resultTerritories.Territories[currentTerritoryKey];
					var validBricksToJoin = GetValidBricksToJoin(currentTerritoryValue, resultTerritories); // Get valid bricks for current solution
					var currentTargetFunctionValue = double.MaxValue;
					var finalBrickToJoin = validBricksToJoin.FirstOrDefault();
					foreach (var validBrick in validBricksToJoin) // for each valid brick
					{
						var tempTargetFunctionValue = CountPartialTargetFunction(currentTerritoryKey, validBrick, resultTerritories);
						if (tempTargetFunctionValue < currentTargetFunctionValue) {
							currentTargetFunctionValue = tempTargetFunctionValue;
							finalBrickToJoin = validBrick;
						}
					}
					if (finalBrickToJoin != null) {
						resultTerritories.AddBrick(currentTerritoryKey, finalBrickToJoin);
						PrintTerritorySolution(resultTerritories);
					}
				}
			}

			Console.WriteLine($"Value of target function: {TargetFunction(resultTerritories)}");
			return resultTerritories;
		}

		private double CountPartialTargetFunction(int currentTerritoryId, Brick<int> newValidBrick, TerritorySolution<int> currentTerritorySolution) {
			var territorySolutionCopy = currentTerritorySolution.DeepClone();
			var tempTerritory = territorySolutionCopy.Territories[currentTerritoryId].AddBrick(newValidBrick);
			return PartialTargetFunction(tempTerritory, territorySolutionCopy);
		}

		private double PartialTargetFunction(Territory<int> newTerritory, TerritorySolution<int> newTerritorySolution) {
			double result = 0;
			for (int i = 0; i < _attributesCount; i++) {
				var averageByManager = GetAttributeAverageByManager(i, newTerritorySolution);
				var attributeSum = GetAttributeSumByAttributeId(i, newTerritory);
				result += Math.Abs(attributeSum - averageByManager);
			}
			return result;
		}

		private double TargetFunction(TerritorySolution<int> territorySolution) {
			double result = 0;
			for (int i = 0; i < _managerCount; i++) {
				for (int j = 0; j < _attributesCount; j++) {
					var averageByManager = GetAttributeAverageByManager(j, territorySolution);
					var attributeSum = GetAttributeSumByAttributeId(i, j, territorySolution);
					result += Math.Abs(attributeSum - averageByManager);
				}
			}
			return result;
		}

		private double GetAttributeSumByAttributeId(int attributeId, Territory<int> newTerritory) {
			return newTerritory.GetAttributesSum(attributeId);
		}

		private double GetAttributeSumByAttributeId(int managerId, int attributeId, TerritorySolution<int> allCurrentTerritories) {
			return allCurrentTerritories.Territories[managerId].GetAttributesSum(attributeId);
		}

		private double GetAttributeAverageByManager(int attributeId, TerritorySolution<int> allCurrentTerritories) {
			var attributeAverageByManager = allCurrentTerritories.Territories.Values
				.Select(territories => territories.GetAttributesSum(attributeId)).Sum();
			attributeAverageByManager /= _managerCount;
			return attributeAverageByManager;
		}

		/// <summary>
		/// Init start territories which consist of one brick per each.
		/// </summary>
		/// <returns></returns>
		private TerritorySolution<int> InitStartBricks() {
			var currentTerritories = new Dictionary<int, Territory<int>>();
			for (int i = 0; i < _managerCount; i++) {
				currentTerritories.Add(i, new Territory<int>(i, _startBricks[i]));
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

		private IEnumerable<Brick<int>> GetAllBricks(Dictionary<int, List<int>> neighborhoodMatrix, Dictionary<int, List<int>> attributiveMatrix) {
			if (neighborhoodMatrix.Count != attributiveMatrix.Count) {
				 throw new ArgumentException("Neighborhood matrix is not suitable to attributive matrix");
			}
			var allBricks = new HashSet<Brick<int>>();
			foreach (var attribute in attributiveMatrix) {
				var attributeDictionary = attribute.Value.Select((value,index) => new {index,value}).ToDictionary(k => k.index,
					v => new Attribute<double>(v.value));
				allBricks.Add(new Brick<int>(attribute.Key, attributeDictionary));
			}
			foreach (var neighborhoods in neighborhoodMatrix) {
				var brick = allBricks.First(x => x.Id == neighborhoods.Key);
				brick.NeighborhoodBricks = allBricks.Where(x => neighborhoods.Value.Contains(x.Id)).ToList();
			}
			return allBricks;
		}

		public void PrintTerritorySolution(TerritorySolution<int> territorySolution) {
			Console.WriteLine("Territories: ");
			foreach (var territory in territorySolution.Territories) {
				Console.WriteLine($"	Manager Id = { territory.Key }. Brick Ids: { string.Join(", ", territory.Value)}");
			}
			Console.WriteLine(new string('-', 20));
		}
	}
}
