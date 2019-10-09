using System;
using System.Collections.Generic;
using System.Text;

namespace TerritoriesAssignment.Core.Algorithms
{
	public class BrickAttributes
	{
		public Dictionary<int, Attribute> Attributes { get; }

		public BrickAttributes(Dictionary<int, Attribute> attributes) {
			Attributes = attributes;
		}
	}
}
