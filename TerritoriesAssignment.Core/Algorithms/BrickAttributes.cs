using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TerritoriesAssignment.Core.Utilities;

namespace TerritoriesAssignment.Core.Algorithms
{
	[Serializable]
	public abstract class BaseBrickAttributes<K,V> : IBrickAttributes<K, V>
	{
		public Dictionary<K, Attribute<V>> AttributeValues { get; }

		public BaseBrickAttributes(Dictionary<K, Attribute<V>> attributes) {
			AttributeValues = attributes;
		}

		public abstract V GetAttributesValueSum();

		public V GetAttributeValue(K attributeId) {
			return AttributeValues[attributeId].Value;
		}

		public int GetAttributesCount() {
			return AttributeValues.Count;
		}

		public object Clone() {
			return this.DeepClone();
		}
	}
}
