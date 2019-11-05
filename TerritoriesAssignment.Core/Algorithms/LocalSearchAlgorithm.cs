using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using TerritoriesAssignment.Core.Utilities;

namespace TerritoriesAssignment.Core.Algorithms
{
	public class LocalSearchAlgorithm<T> : Problem<T>
	{
		private readonly ICollection<Brick<T>> _allBricks;
		private readonly Dictionary<T, List<T>> _neighborhoodMatrix; //n * n
		private readonly Dictionary<T, Dictionary<T, double>> _attributiveMatrix; // n * k		brickId - (attributeId - attributeValue)
		protected TerritorySolution<T> StartSolution { get; set; }

		public LocalSearchAlgorithm(TerritorySolution<T> startSolution, List<T> managerIds, List<T> attributeIds, 
			Dictionary<T, List<T>> neighborhoodMatrix, Dictionary<T, Dictionary<T, double>> attributiveMatrix) {
			StartSolution = startSolution;
			_allBricks = GetAllBricks(neighborhoodMatrix, attributiveMatrix);
			_neighborhoodMatrix = neighborhoodMatrix;
			_attributiveMatrix = attributiveMatrix;
			_managerIds = managerIds;
			_attributeIds = attributeIds;
		}

		public override TerritorySolution<T> Solve() {
			var resultSolution = GetStartSolution();
			int withoutImprovement = 0;
			var counterValueToStop = GetCounterValueToStop();
			while (withoutImprovement != counterValueToStop) {
				for (int i = 0; i < _managerIds.Count; i++) {
					for (int j = i + 1; j < _managerIds.Count; j++) {
						Console.WriteLine($"{_managerIds[i]} {_managerIds[j]}");
						var donorManager = _managerIds[i];
						var recipientManager = _managerIds[j];

						BrickExchange(resultSolution.Territories[donorManager],
							resultSolution.Territories[recipientManager]); // i -> j

						BrickExchange(resultSolution.Territories[donorManager],
							resultSolution.Territories[recipientManager]); // i <- j

					}
				}
			}
			return resultSolution;
		}

		protected virtual int GetCounterValueToStop() {
			return _managerIds.Count * (_managerIds.Count - 1) / 2;
		}

		protected virtual void BrickExchange(Territory<T> donorTerritory, Territory<T> recipientTerritory) {
			var borderDonorBricks = GetDonorBorderBricks(donorTerritory, recipientTerritory);
			var donorTerritoryClone = donorTerritory.DeepClone();
			var recipientTerritoryClone = recipientTerritory.DeepClone();
			for (int i = 0; i < borderDonorBricks.Count; i++) {
				donorTerritoryClone.RemoveBrick(borderDonorBricks[i]);
				if (donorTerritoryClone.IsAllBricksConnected()) {
					
				}
			}
		}

		protected virtual IEnumerable<Brick<T>> GetValidDonorBricks(Territory<T> donorTerritory) {
			var validDonorBricks = new List<Brick<T>>();
			
			var newDonor = donorTerritory.Bricks.Select(brick =>
				brick.NeighborhoodBricks.RemoveAll(neighBrick => donorTerritory.Bricks.Contains(neighBrick)));
			//var connectedDonorBricks = GetConnectedBricksDFS(donorTerritory.Bricks.);

			return validDonorBricks;
		}

		

		private IList<Brick<T>> GetValidBorderBricks(Territory<T> donorTerritory, Territory<T> recipientTerritory) {
			var validDonorBricks = GetValidDonorBricks(donorTerritory);
			var borderDonorBricks = new List<Brick<T>>();

			return borderDonorBricks;
		}

		private IList<Brick<T>> GetDonorBorderBricks(Territory<T> donorTerritory, Territory<T> recipientTerritory) {
			var borderDonorBricks = new List<Brick<T>>();
			for (int i = 0; i < donorTerritory.GetBricksCount(); i++) {
				var currDonorBrick = donorTerritory.Bricks[i];
				for (int j = 0; j < recipientTerritory.GetBricksCount(); j++) {
					if (currDonorBrick.IsNeighbor(recipientTerritory.Bricks[j])) {
						borderDonorBricks.Add(currDonorBrick);
						break;
					}
				}
			}
			return borderDonorBricks;
		}

		protected virtual TerritorySolution<T> GetStartSolution() {
			return StartSolution;
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
	}
}
