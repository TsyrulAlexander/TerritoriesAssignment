using System;
using System.Collections.Generic;
using System.Text;
using TerritoriesAssignment.Core.Utilities;

namespace TerritoriesAssignment.Core.Algorithms
{
	[Serializable]
	public class Brick<T> : ICloneable, IEquatable<T>
	{
		public T Id { get; }
		public List<Brick<T>> NeighborhoodBricks { get; set; }

		public IBrickAttributes<T, double> Attributes { get; set; }

		public Brick(T brickId, Dictionary<T, Attribute<double>> attributes = null, List<Brick<T>> neighborhoodBricks = null) {
			Id = brickId;
			NeighborhoodBricks = neighborhoodBricks;
			Attributes = new DoubleBrickAttributes<T>(attributes);
		}

		public bool IsNeighbor(Brick<T> brick) {
			return NeighborhoodBricks.Exists(x => x.Equals(brick));
		}

		public bool Equals(T other) {
			return Id.Equals(other);
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

		public override string ToString() {
			return Id.ToString();
		}
	}
}
