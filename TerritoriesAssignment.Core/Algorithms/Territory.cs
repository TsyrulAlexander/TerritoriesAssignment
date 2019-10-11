using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerritoriesAssignment.Core.Utilities;

namespace TerritoriesAssignment.Core.Algorithms
{
	[Serializable]
	public class Territory<T> : ICloneable
	{
		public T Id { get; }		//Manager Id
		public HashSet<Brick<T>> Bricks { get; }

		public Territory(T territoryId, Brick<T> firstBrick) {
			Id = territoryId;
			Bricks = new HashSet<Brick<T>> { firstBrick };
		}

		public Territory(T territoryId, IEnumerable<Brick<T>> bricks) {
			Id = territoryId;
			Bricks = new HashSet<Brick<T>>(bricks);
		}

		public virtual int GetBricksCount() {
			return Bricks.Count;
		}

		public virtual IEnumerable<T> GetBrickIds() {
			return Bricks.Select(x => x.Id);
		}

		public double GetAttributesSum() {
			//return Bricks.Select(x => x.Attributes.Values.Select(y => y.Value).Sum()).Sum();
			return Bricks.Select(brick => brick.Attributes.GetAttributesValueSum()).Sum();
		}

		public double GetAttributesSum(T attributeId) {
			//return Bricks.Select(x => x.Attributes[].Value).Sum();
			return Bricks.Select(brick => brick.Attributes.GetAttributeValue(attributeId)).Sum();
		}

		public virtual void AddBrick(Brick<T> brick) {
			Bricks.Add(brick);
		}

		public object Clone() {
			return this.DeepClone();
		}
	}
}
