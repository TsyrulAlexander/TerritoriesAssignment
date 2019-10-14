using System;
using System.Collections.Generic;
using System.Text;
using TerritoriesAssignment.Core.Utilities;

namespace TerritoriesAssignment.Core.Algorithms
{
	[Serializable]
	public class Attribute<T> : ICloneable
	{
		public T Value { get; }

		public Attribute(T value) {
			Value = value;
		}

		public object Clone() {
			return this.DeepClone();
		}
	}
}
