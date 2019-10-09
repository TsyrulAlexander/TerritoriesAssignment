using System;
using System.Collections.Generic;
using System.Text;

namespace TerritoriesAssignment.Core.Algorithms
{
	public class TerritorySolution<T>
	{
		protected List<Territory<T>> Territories { get; }

		public TerritorySolution(List<Territory<T>> startTerritories) {
			Territories = startTerritories;
		}
	}
}
