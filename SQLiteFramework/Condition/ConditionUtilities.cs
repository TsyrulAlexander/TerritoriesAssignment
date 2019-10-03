using System;
using System.Collections.Generic;
using System.Text;
using SQLiteFramework.Condition.Column;
using SQLiteFramework.Condition.Value;

namespace SQLiteFramework.Condition {
	public class ConditionUtilities {
		public IConditionValue Value(string value) {
			return new StringConditionValue(value);
		}
		public IConditionValue Value(Guid value) {
			return new GuidConditionValue(value);
		}
		public SQLiteColumn<string> StringColumn(string columnName) {
			return new SQLiteColumn<string>();
		}
		public SQLiteColumn<Guid> GuidColumn(string columnName) {
			return new SQLiteColumn<Guid>();
		}
	}
}