namespace TerritoriesAssignment.Core.Algorithms
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Text;

	[Serializable]
	public class DoubleBrickAttributes<T> : BaseBrickAttributes<T, double>
	{
		public DoubleBrickAttributes(Dictionary<T, Attribute<double>> attributes) : base(attributes) { }

		public override double GetAttributesValueSum() {
			return AttributeValues.Values.Select(x => x.Value).Sum();
		}
	}
}
