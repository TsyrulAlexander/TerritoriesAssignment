using System;
using System.Collections.Generic;
using System.Text;
using SQLiteFramework.Condition.Value;
using SQLiteFramework.Table;

namespace SQLiteFramework.Condition.Column {
	public class SQLiteColumnValue : SQLiteColumn {
		public IConditionValue Value { get; set; }
		public SQLiteColumnValue(string name, SQLiteColumnType type, IConditionValue value) : base(name, type) {
			Value = value;
		}
		public SQLiteColumnValue(string name, object value) : base(name, value?.GetType()) {
			Value = GetConditionValue(value);
		}
		public SQLiteColumnValue(string name) : base(name) { }
		protected IConditionValue GetConditionValue(object value) {
			if (value == null) {
				return new NullConditionValue();
			}
			var type = value.GetType();
			if (type == typeof(Guid)) {
				return new GuidConditionValue((Guid) value);
			}
			if (type == typeof(string)) {
				return new StringConditionValue((string) value);
			}
			throw new NotSupportedException(type.Name);
		}
	}
}