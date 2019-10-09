using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TerritoriesAssignment.Core.Algorithms
{
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

		public virtual void AddBrick(Brick<T> brick) {
			Bricks.Add(brick);
		}

		public object Clone() {
			var bricks = new HashSet<Brick<T>>();
			foreach (var brick in Bricks) {
				bricks.Add(new Brick<T>(brick.Id));
			}
			return new Territory<T>(Id, bricks); 
		}
	}
}
