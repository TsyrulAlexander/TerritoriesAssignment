using System;
using System.Collections.Generic;
using System.Text;

namespace TerritoriesAssignment.Core.Algorithms
{
	public class Brick<T>
	{
		public T Id { get; }
		public IEnumerable<Brick<T>> NeighborhoodBricks { get; }
		//public IEnumerable<Attribute> Attributes { get; }
		public BrickAttributes Attributes { get; set; }

		public Brick(T brickId, IEnumerable<Brick<T>> neighborhoodBricks = null, BrickAttributes attributes = null) {
			Id = brickId;
			NeighborhoodBricks = neighborhoodBricks;
			Attributes = attributes;
		}

		public override bool Equals(object brickObj) {
			if (brickObj == null)
				return false;
			var brick = brickObj as Brick<T>;
			if (brick == null)
				return false;
			return Id.Equals(brick.Id);
		}

		public override int GetHashCode() {
			return Id.GetHashCode();
		}
	}
}
