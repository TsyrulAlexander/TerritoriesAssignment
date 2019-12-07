using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Schema;
using TerritoriesAssignment.Core.Utilities;

namespace TerritoriesAssignment.Core.Algorithms
{
	class Experiment
	{
		public double FirstAlgExecutionTime {
			get; set;
		}
		public double SecondAlgExecutionTime {
			get; set;
		}
	}
	public class LocalSearchAlgorithm<T> : Problem<T>
	{
		private readonly ICollection<Brick<T>> _allBricks;
		private readonly Dictionary<T, List<T>> _neighborhoodMatrix; //n * n
		private readonly Dictionary<T, Dictionary<T, double>> _attributiveMatrix; // n * k		brickId - (attributeId - attributeValue)
		protected TerritorySolution<T> StartSolution { get; set; }
		protected TerritorySolution<T> ResultSolution { get; set; }

		public LocalSearchAlgorithm(TerritorySolution<T> startSolution, List<T> managerIds, List<T> attributeIds, 
			Dictionary<T, List<T>> neighborhoodMatrix, Dictionary<T, Dictionary<T, double>> attributiveMatrix) {
			StartSolution = startSolution;
			_allBricks = GetAllBricks(neighborhoodMatrix, attributiveMatrix);
			_neighborhoodMatrix = neighborhoodMatrix;
			_attributiveMatrix = attributiveMatrix;
			_managerIds = managerIds;
			_attributeIds = attributeIds;
			ResultSolution = startSolution;
		}

		public override TerritorySolution<T> Solve() {
			ResultSolution = GetStartSolution();
			var watch = System.Diagnostics.Stopwatch.StartNew();
			int withoutImprovement = 0;
			var counterValueToStop = GetCounterValueToStop();
			while (withoutImprovement != counterValueToStop) {
				withoutImprovement = 0;
				for (int i = 0; i < _managerIds.Count; i++) {
					for (int j = i + 1; j < _managerIds.Count; j++) {
						var firstManagerId = _managerIds[i];
						var secondManagerId = _managerIds[j];
						//Console.WriteLine($"1-st manager {firstManagerId}, 2-nd manager {secondManagerId}");
						if (BrickExchangeSuccessful(firstManagerId, secondManagerId)) {		// i -> j
							withoutImprovement = 0;
						}
						else { withoutImprovement++; } 
						if (BrickExchangeSuccessful(secondManagerId, firstManagerId)) {		// i <- j
							withoutImprovement = 0;
						} 
						else { withoutImprovement++; }
					}
				}
			}
			watch.Stop();
			ResultSolution.ElapsedTime = watch.ElapsedMilliseconds;
			return ResultSolution;
		}

		public double TargetFunction() {
			return base.TargetFunction(ResultSolution);
		}

		protected virtual int GetCounterValueToStop() {
			return _managerIds.Count * (_managerIds.Count - 1);
		}

		protected virtual bool BrickExchangeSuccessful(T donorManagerId, T recipientManagerId) {
			var tempTerritorySolution = ResultSolution.DeepClone();
			var borderDonorBricks = tempTerritorySolution.GetDonorBorderBricks(donorManagerId, recipientManagerId);
			for (var i = 0; i < borderDonorBricks.Count; i++) {
				tempTerritorySolution.MoveBrick(donorManagerId, recipientManagerId, borderDonorBricks[i]);
				if (tempTerritorySolution.Territories[donorManagerId].IsAllBricksConnected()) {
					var tempTargetFuncValue = TargetFunction(tempTerritorySolution);
					var bestTargetFuncValue = TargetFunction(ResultSolution);
					if (tempTargetFuncValue < bestTargetFuncValue) {
						ResultSolution.MoveBrick(donorManagerId, recipientManagerId, borderDonorBricks[i]);
						//PrintTerritorySolution(ResultSolution);
						//Console.Write("Improvement found");
						return true;
					}
				} else {
					tempTerritorySolution.MoveBrick( recipientManagerId, donorManagerId, borderDonorBricks[i]);
				}
			}
			//Console.Write("No improvement");
			return false;
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
