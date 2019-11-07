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
		public IList<Brick<T>> Bricks { get; } //TODO: change list to dictionary

		public Territory(T territoryId, Brick<T> firstBrick) {
			Id = territoryId;
			Bricks = new List<Brick<T>> { firstBrick };
		}

		public Territory(T territoryId, IEnumerable<Brick<T>> bricks) {
			Id = territoryId;
			Bricks = new List<Brick<T>>(bricks);
		}

		public virtual bool BelongsToTheTerritory(Brick<T> brick) {
			return Bricks.Contains(brick);
		}

		public virtual int GetBricksCount() {
			return Bricks.Count;
		}

		public virtual IEnumerable<T> GetBrickIds() {
			return Bricks.Select(x => x.Id);
		}

		public double GetAttributesSum() {
			return Bricks.Select(brick => brick.Attributes.GetAttributesValueSum()).Sum();
		}

		public double GetAttributesSum(T attributeId) {
			return Bricks.Select(brick => brick.Attributes.GetAttributeValue(attributeId)).Sum();
		}

		public virtual Territory<T> AddBrick(Brick<T> brick) {
			if (!Bricks.Contains(brick)) {
				Bricks.Add(brick);
			} else {
				Console.WriteLine($"Add brick error. Territory {Id} already contains brick {brick.Id}");
			}
			return this;
		}

		public void RemoveBrick(Brick<T> brick) {
			if (!Bricks.Remove(brick)) {
				Console.WriteLine($"Remove brick error. Territory {Id} does not contain brick {brick.Id}");
			}
		}

		public bool IsAllBricksConnected() {//DFS
			if (GetBricksCount() == 0) {
				return false;
			}
			var stackOfBricks = new Stack<Brick<T>>();
			var firstBrick = Bricks.First();
			stackOfBricks.Push(firstBrick);
			var visitedBricks = new List<Brick<T>> { firstBrick };
			while (stackOfBricks.Count != 0) {
				var peekBrick = stackOfBricks.Peek();
				var firstNeighbor = peekBrick.NeighborhoodBricks.FirstOrDefault(x => !visitedBricks.Contains(x) && BelongsToTheTerritory(x));	//TODO: Check belonging current brick current territory. Search in dictionary of bricks
				if (firstNeighbor != null) {
					stackOfBricks.Push(firstNeighbor);
					visitedBricks.Add(firstNeighbor);
				} else {
					stackOfBricks.Pop();
				}
			}
			return visitedBricks.Count == GetBricksCount();
		}

		public object Clone() {
			return this.DeepClone();
		}

		public override string ToString() {
			return string.Join(", ", Bricks.Select(brick => brick.ToString()).ToArray());
		}
	}
}
