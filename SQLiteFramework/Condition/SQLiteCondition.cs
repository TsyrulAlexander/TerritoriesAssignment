using System;
using System.Collections.Generic;
using System.Text;
using SQLiteFramework.Condition.Value;

namespace SQLiteFramework.Condition {
	public class SQLiteCondition {
		public string ColumnPath { get; set; }
		public SQLiteComparisonType ComparisonType { get; set; }
		public IConditionValue Value { get; set; }
	}
}
