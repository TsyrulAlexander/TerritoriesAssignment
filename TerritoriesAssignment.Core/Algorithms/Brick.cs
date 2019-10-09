using System;
using System.Collections.Generic;
using System.Text;

namespace TerritoriesAssignment.Core.Algorithms
{
	public class Brick<T>
	{
		public T Id { get; }

		public Brick(T brickId) {
			Id = brickId;
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
