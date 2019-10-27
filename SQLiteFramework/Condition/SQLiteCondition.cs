using System;
using SQLiteFramework.Condition.Value;

namespace SQLiteFramework.Condition {
	public class SQLiteCondition: ISQLiteCondition {
		public string ColumnPath { get; set; }
		public SQLiteComparisonType ComparisonType { get; set; }
		public IConditionValue Value { get; set; }
		public string[] GetJoinPath() {
			return new []{ ColumnPath };
		}
		public virtual string GetSqlText(string tableName) {
			if (SQLiteUtilities.GetJoinPath(ColumnPath, out var info, out var lastColumnName)) {
				var lastJoin = info[info.Length - 1];
				return $" {lastJoin.TableName}.{lastColumnName} {ToString(ComparisonType, Value)} ";
			}
			return $" {tableName}.{ColumnPath} {ToString(ComparisonType, Value)} ";
		}
		public SQLiteCondition(string columnPath = null, 
			SQLiteComparisonType comparisonType = SQLiteComparisonType.Equal,
			IConditionValue value = null) {
			ColumnPath = columnPath;
			ComparisonType = comparisonType;
			Value = value;
		}
		public static string ToString(SQLiteComparisonType comparisonType, IConditionValue value) {
			switch (comparisonType) {
				case SQLiteComparisonType.Equal:
					return $"= {value.GetValue()}";
				case SQLiteComparisonType.NotEqual:
					return $"!= {value.GetValue()}";
				case SQLiteComparisonType.Contains:
					return $"LIKE '%{value.GetValue()}%'";
				default:
					throw new NotSupportedException(nameof(comparisonType));
			}
		}
	}
}
