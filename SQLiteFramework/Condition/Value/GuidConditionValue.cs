using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteFramework.Condition.Value {
	public class GuidConditionValue : IConditionValue {
		public Guid Value { get; }
		public GuidConditionValue(Guid value) {
			Value = value;
		}
		public object GetValue() {
			return Value;
		}
	}
}
