using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using TerritoriesAssignment.Core.Utilities;

namespace TerritoriesAssignment.Core.Algorithms
{
	public class SequentialHeuristicAlgorithm<T>
	{
		private readonly List<T> _managerIds;
		private readonly List<T> _attributeIds;
		private readonly int _bricksCount;
		private readonly ICollection<Brick<T>> _allBricks;
		private readonly List<Brick<T>> _startBricks;
		private readonly Dictionary<T, List<T>> _neighborhoodMatrix; //n * n
		private readonly Dictionary<T, Dictionary<T, double>> _attributiveMatrix; // n * k		brickId - attributeId  

		public SequentialHeuristicAlgorithm(List<T> managerIds, List<T> attributeIds, Dictionary<T, List<T>> neighborhoodMatrix, 
			Dictionary<T, Dictionary<T, double>> attributiveMatrix, IEnumerable<T> startBrickIds) {
			_bricksCount = neighborhoodMatrix.Count;
			_allBricks = GetAllBricks(neighborhoodMatrix, attributiveMatrix);
			_startBricks = _allBricks.Where(brick => startBrickIds.Contains(brick.Id)).ToList();
			_neighborhoodMatrix = neighborhoodMatrix;
			_attributiveMatrix = attributiveMatrix;
			_managerIds = managerIds;
			_attributeIds = attributeIds;
		}

		public bool IsValidAlgorithmParameters(out string exceptionMessage) {
			exceptionMessage = string.Empty;
			if (_attributiveMatrix.Values.All(att => att.Count.Equals(_attributeIds.Count))) {
				exceptionMessage += "Attributes count in matrix is not equal attribute Ids count. ";
			}
			if (_attributiveMatrix.Count != _neighborhoodMatrix.Count) {
				exceptionMessage += "Some bricks have not attributes. ";
			}
			if (_neighborhoodMatrix.Values.Any(brick => brick.Count == 0)) {
				exceptionMessage += "Some of bricks have not any neighborhood. ";
			}
			if (_startBricks.Count == 0) {
				exceptionMessage += "There is no start bricks. ";
			}
			if (_startBricks.Count != _managerIds.Count) {
				exceptionMessage += "Start bricks count must be equal manager Ids count. ";
			}
			if (_neighborhoodMatrix.Count <= _managerIds.Count) {
				exceptionMessage += "Manager count must be less than bricks count. ";
			}
			return exceptionMessage.Equals(string.Empty);
		}

		public TerritorySolution<T> Solve() {	 // m * n
			var	resultTerritories = InitStartBricks();
			PrintTerritorySolution(resultTerritories);
			while (resultTerritories.GetBricksCount() != _allBricks.Count) //while all bricks will be assigned
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

		private double CountPartialTargetFunction(T currentTerritoryId, Brick<T> newValidBrick, TerritorySolution<T> currentTerritorySolution) {
			var territorySolutionCopy = currentTerritorySolution.DeepClone();
			var tempTerritory = territorySolutionCopy.Territories[currentTerritoryId].AddBrick(newValidBrick);
			return PartialTargetFunction(tempTerritory, territorySolutionCopy);
		}

		private double PartialTargetFunction(Territory<T> newTerritory, TerritorySolution<T> newTerritorySolution) {
			double result = 0;
			foreach (var attributeId in _attributeIds) {
				var averageByManager = GetAttributeAverageByManager(attributeId, newTerritorySolution);
				var attributeSum = GetAttributeSumByAttributeId(attributeId, newTerritory);
				result += Math.Abs(attributeSum - averageByManager);
			}
			return result;
		}

		private double TargetFunction(TerritorySolution<T> territorySolution) {
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

		private double GetAttributeSumByAttributeId(T attributeId, Territory<T> newTerritory) {
			return newTerritory.GetAttributesSum(attributeId);
		}

		private double GetAttributeSumByAttributeId(T managerId, T attributeId, TerritorySolution<T> allCurrentTerritories) {
			return allCurrentTerritories.Territories[managerId].GetAttributesSum(attributeId);
		}

		private double GetAttributeAverageByManager(T attributeId, TerritorySolution<T> allCurrentTerritories) {
			var attributeAverageByManager = allCurrentTerritories.Territories.Values
				.Select(territories => territories.GetAttributesSum(attributeId)).Sum();
			attributeAverageByManager /= _managerIds.Count;
			return attributeAverageByManager;
		}

		/// <summary>
		/// Init start territories which consist of one brick per each.
		/// </summary>
		/// <returns></returns>
		private TerritorySolution<T> InitStartBricks() {
			var currentTerritories = new Dictionary<T, Territory<T>>();
			for (int i = 0; i < _managerIds.Count; i++) {
				currentTerritories.Add(_managerIds[i], new Territory<T>(_managerIds[i], _startBricks[i]));
			}
			return new TerritorySolution<T>(currentTerritories);
		}

		private IEnumerable<Brick<T>> GetValidBricksToJoin(Territory<T> currentTerritory,
			TerritorySolution<T> allCurrentTerritories) {
			var validBricks = new HashSet<Brick<T>>();
			foreach (var brick in currentTerritory.Bricks) {
				validBricks.UnionWith(brick.NeighborhoodBricks);
			}
			validBricks.ExceptWith(currentTerritory.Bricks);
			validBricks.ExceptWith(allCurrentTerritories.GetAllBricks());
			return validBricks.ToList();
		}

		private ICollection<Brick<T>> GetAllBricks(Dictionary<T, List<T>> neighborhoodMatrix, Dictionary<T, Dictionary<T, double>> attributiveMatrix) {
			if (neighborhoodMatrix.Count != attributiveMatrix.Count) {
				 throw new ArgumentException("Neighborhood matrix is not suitable to attributive matrix");
			}
			var allBricks = new HashSet<Brick<T>>();
			foreach (var brickKey in attributiveMatrix.Keys) {
				var attributeDictionary = attributiveMatrix[brickKey].ToDictionary(k => k.Key,
					v => new Attribute<double>(v.Value));
				allBricks.Add(new Brick<T>(brickKey, attributeDictionary));
			}
			foreach (var neighborhoods in neighborhoodMatrix) {
				var brick = allBricks.First(x => x.Id.Equals(neighborhoods.Key));
				brick.NeighborhoodBricks = allBricks.Where(x => neighborhoods.Value.Contains(x.Id)).ToList();
			}
			return allBricks;
		}

		public void PrintTerritorySolution(TerritorySolution<T> territorySolution) {
			Console.WriteLine("Territories: ");
			foreach (var territory in territorySolution.Territories) {
				Console.WriteLine($"	Manager Id = { territory.Key }. Brick Ids: { string.Join(", ", territory.Value)}");
			}
			Console.WriteLine(new string('-', 20));
		}
	}
}
