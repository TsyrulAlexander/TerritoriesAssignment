using System;
using System.Collections.Generic;
using System.Text;
using TerritoriesAssignment.Core.Entities;

namespace TerritoriesAssignment.Core.Algorithms
{
	public class CombinatorialOptimization 
	{
		public IStrategy CurrentStrategy { get; set; }

		public CombinatorialOptimization(IStrategy contextStrategy) {
			CurrentStrategy = contextStrategy;
		}

		public void GetTerritoryPartitioning() {
			CurrentStrategy.Algorithm();
		}
	}
}
