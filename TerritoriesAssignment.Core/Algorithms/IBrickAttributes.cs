using System;
using System.Collections.Generic;
using System.Text;

namespace TerritoriesAssignment.Core.Algorithms
{
	public interface IBrickAttributes<K, V> : ICloneable
	{
		Dictionary<K, Attribute<V>> AttributeValues { get; }
		V GetAttributesValueSum();
		V GetAttributeValue(K attributeId);
		int GetAttributesCount();
	}
}
