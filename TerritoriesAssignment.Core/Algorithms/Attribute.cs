using System;
using System.Collections.Generic;
using System.Text;

namespace TerritoriesAssignment.Core.Algorithms
{
	public class Attribute<T> : ICloneable
	{
		public T Value { get; }

		public Attribute(T value) {
			Value = value;
		}

		public object Clone() {
			return new Attribute<T>(Value);
		}
	}
}
