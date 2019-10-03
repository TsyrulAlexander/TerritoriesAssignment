using System;
using System.Collections.Generic;
using System.Text;

namespace SQLiteFramework.Condition.Value {
	public class StringConditionValue : IConditionValue {
		public string Value { get; }
		public StringConditionValue(string value) {
			Value = value;
		}
		public object GetValue() {
			return Value;
		}
	}
}
