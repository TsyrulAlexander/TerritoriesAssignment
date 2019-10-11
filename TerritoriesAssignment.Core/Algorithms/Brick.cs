using System;
using System.Collections.Generic;
using System.Text;
using TerritoriesAssignment.Core.Utilities;

namespace TerritoriesAssignment.Core.Algorithms
{
	[Serializable]
	public class Brick<T> : ICloneable
	{
		public T Id { get; }
		public IEnumerable<Brick<T>> NeighborhoodBricks { get; }
		//public IEnumerable<Attribute> Attributes { get; }
		public IBrickAttributes<T, double> Attributes { get; set; }
		//public Dictionary<T, Attribute<double>> Attributes { get; set; }

		public Brick(T brickId, IEnumerable<Brick<T>> neighborhoodBricks = null, Dictionary<T, Attribute<double>> attributes = null) {
			Id = brickId;
			NeighborhoodBricks = neighborhoodBricks;
			//Attributes = attributes;
			Attributes = new DoubleBrickAttributes<T>(attributes);
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

		public object Clone() {
			return this.DeepClone();
		}
	}
}
