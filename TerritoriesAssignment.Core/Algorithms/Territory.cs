using System;
using System.Collections.Generic;
using System.Text;

namespace TerritoriesAssignment.Core.Algorithms
{
	public class Territory<T>
	{
		public T Id { get; }
		public HashSet<Brick<T>> Bricks { get; }

		public Territory(T territoryId, Brick<T> firstBrick) {
			Id = territoryId;
			Bricks = new HashSet<Brick<T>> { firstBrick };
		}

		public Territory(T territoryId, IEnumerable<Brick<T>> bricks) {
			Id = territoryId;
			Bricks = new HashSet<Brick<T>>(bricks);
		}
	}
}
